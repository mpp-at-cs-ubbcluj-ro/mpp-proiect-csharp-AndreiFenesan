using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using model;
using Networking.requests;
using Networking.responses;
using Servicies;

namespace Networking;

public class ClientWorker:IObserver
{
    private readonly TcpClient _client;
    private readonly IService _service;
    private volatile bool _connected;

    public ClientWorker(TcpClient client, IService service)
    {
        _client = client;
        _service = service;
        this._connected = true;
    }

    [Obsolete("Obsolete")]
    public void Run()
    {
        IFormatter formatter = new BinaryFormatter();
        using (_client)
        {
            using (NetworkStream stream = this._client.GetStream())
            {
                while (_connected)
                {
                    try
                    {
                        IRequest request = (IRequest)formatter.Deserialize(stream);
                        IResponse? response = ProcessRequest(request);
                        if (response != null)
                        {
                            SendResponse(response, stream, formatter);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        _client.Close();
                        _connected = false;
                    }
                }
                Console.WriteLine("Worker done");
            }
        }
    }

    [Obsolete("Obsolete")]
    private void SendResponse(IResponse response, NetworkStream stream, IFormatter formatter)
    {
        formatter.Serialize(stream, response);
    }

    private IResponse? ProcessRequest(IRequest request)
    {
        if (request is GetAllCharityCaseRequest caseRequest)
        {
            return ProcessGetAllCharityRequest(caseRequest);
        }

        if (request is LoginRequest loginRequest)
        {
            return ProcessLoginRequest(loginRequest);
        }

        if (request is GetDonorWithNameLikeRequest getDonorWithNameLikeRequest)
        {
            return ProcessGetDonorWithNameLikeRequest(getDonorWithNameLikeRequest);
        }

        if (request is NewDonationRequest donationRequest)
        {
            return ProcessNewDonationRequest(donationRequest);
        }

        if (request is LogOutRequest)
        {
            this._connected = false;
            return null;
        }

        return null;
    }

    private IResponse ProcessNewDonationRequest(NewDonationRequest donationRequest)
    {
        try
        {
            bool addedDonation = _service.AddNewDonation(donationRequest.CharityCaseId, donationRequest.Name,
                donationRequest.Email,
                donationRequest.PhoneNumber, donationRequest.Amount);
            if (addedDonation)
            {
                return new ResponseOk();
            }
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }

        return new ErrorResponse("Error");
    }

    private IResponse ProcessGetDonorWithNameLikeRequest(GetDonorWithNameLikeRequest request)
    {
        IEnumerator<Donor> donors = _service.GetDonorsWithNameContaining(request.NameContains);
        List<DonorDto> donorDtos = new List<DonorDto>();
        while (donors.MoveNext())
        {
            Donor donor = donors.Current;
            DonorDto donorDto = new DonorDto(donor.Name, donor.EmailAddress, donor.PhoneNumber);
            donorDtos.Add(donorDto);
        }

        return new GetDonorsWithNameLikeResponse(donorDtos);
    }

    private IResponse ProcessLoginRequest(LoginRequest request)
    {
        bool isVolunteerValid = _service.AuthenticateVolunteer(request.Username, request.Password);
        if (isVolunteerValid)
        {
            return new ResponseOk();
        }

        return new ErrorResponse("Invalid credentials");
    }

    private IResponse ProcessGetAllCharityRequest(GetAllCharityCaseRequest request)
    {
        List<CharityCaseDto> cases = this._service.GetAllCharityCases();
        IResponse response = new CharityCaseResponse(cases);
        return response;
    }

    public void UpdateEver()
    {
        Console.WriteLine("Sending update response");
        IFormatter formatter = new BinaryFormatter();
        SendResponse(new UpdateResponse(),_client.GetStream(),formatter);
    }
}