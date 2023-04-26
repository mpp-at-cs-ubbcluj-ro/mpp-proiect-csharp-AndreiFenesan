using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using model;
using Networking.requests;
using Networking.responses;
using Servicies;

namespace Networking;

public class ServiceClientProxy : IService,IObservable
{
    
    private TcpClient _serverSocket;
    private string _hostAddress;
    private int _port;
    private Queue<IResponse> _responses;
    private IFormatter _formatter;
    private EventWaitHandle _waitHandle;
    private volatile bool _finished;
    private List<IObserver> _clients;

    public ServiceClientProxy()
    {
        this._serverSocket = null;
        this._port = 10102;
        this._hostAddress = "127.0.0.1";
        this._responses = new Queue<IResponse>();
        this._formatter = new BinaryFormatter();
        this._waitHandle = new AutoResetEvent(false);
        this._finished = false;
        this._clients = new List<IObserver>();
    }

    public bool AuthenticateVolunteer(string username, string password)
    {
        if (this._serverSocket is null)
        {
            InitialiseConnection();
        }
        SendRequest(new LoginRequest(username,password));
        IResponse response = ReadResponse();
        if (response is ResponseOk)
        {
            return true;
        }
        return false;
    }

    [Obsolete("Obsolete")]
    private void SendRequest(IRequest request)
    {
        try
        {
            Console.WriteLine("Sending request");
            this._formatter.Serialize(_serverSocket.GetStream(), request);
            _serverSocket.GetStream().Flush();
            Console.WriteLine("Request sent");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error in sending request");
        }
    }


    public bool AddNewDonation(long charityCaseId, string name, string emailAddress, string phoneNumber,
        double donationAmount)
    {
        SendRequest(new NewDonationRequest(charityCaseId,name,emailAddress,phoneNumber,donationAmount));
        IResponse response = ReadResponse();
        if (response is ResponseOk)
        {
            return true;
        }
        if (response is ErrorResponse errorResponse)
        {
            throw new ServiceException(errorResponse.Message);
        }

        return false;
    }

    public IEnumerator<Donor> GetDonorsWithNameContaining(string containsInName)
    {
        SendRequest(new GetDonorWithNameLikeRequest(containsInName));
        IResponse response = ReadResponse();
        if (response is GetDonorsWithNameLikeResponse nameLikeResponse)
        {
            var donors =nameLikeResponse.Donors;
            List<Donor> correctDonors = new List<Donor>();
            foreach (var donorDto in donors)
            {
                correctDonors.Add(new Donor(donorDto.Name,donorDto.Email,donorDto.PhoneNumber));
            }
            
            return correctDonors.GetEnumerator();
        }

        return null;
    }

    public List<CharityCaseDto> GetAllCharityCases()
    {
        Console.WriteLine("Getting all charity cases");
        SendRequest(new GetAllCharityCaseRequest());
        IResponse response = ReadResponse();
        if (response is CharityCaseResponse charityCaseResponse)
        {
            return charityCaseResponse.Cases;
        }
        Console.WriteLine("Not response we want");
        return null;
    }

    private IResponse? ReadResponse()
    {
        this._waitHandle.WaitOne();
        try
        {
            lock (this._responses)
            {
                return this._responses.Dequeue();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error {e.Message}");
            return null;
        }
    }

    [Obsolete("Obsolete")]
    private void Run()
    {
        Console.WriteLine("Reader staring");
        while (!_finished)
        {
            try
            {
                IResponse response = (IResponse)_formatter.Deserialize(_serverSocket.GetStream());
                Console.WriteLine("Response received");
                Console.WriteLine("Total of {0} observers",_clients.Count);
                if (response is UpdateResponse)
                {
                    Console.WriteLine("Update response received");
                    Console.WriteLine($"Notifiying {0}",_clients.Count);
                    NotifyAllObservers();
                }
                else
                {
                    lock (this._responses)
                    {
                        _responses.Enqueue(response);
                    }
                    _waitHandle.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in Run method {e.Message}");
            }
        }
    }

    private void StartReader()
    {
        Thread reader = new Thread(Run);
        reader.Start();
    }

    private void InitialiseConnection()
    {
        try
        {
            this._serverSocket = new TcpClient(this._hostAddress, this._port);
            this._finished = false;
            this.StartReader();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in connection: {e.Message}");
        }
    }

    public void AddObserver(IObserver observer)
    {
        Console.WriteLine("Adding obersver");
        this._clients.Add(observer);
        Console.WriteLine("Total of {0} observers",_clients.Count);

    }

    public void RemoveObserver(IObserver observer)
    {
        this._clients.Remove(observer);
    }

    public void NotifyAllObservers()
    {
        foreach (var observer in this._clients)
        {
            observer.UpdateEver();
        }
    }
}