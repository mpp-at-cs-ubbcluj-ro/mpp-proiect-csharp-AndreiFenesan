using System.Net;
using System.Net.Sockets;
using servicies;

namespace server.servers;

public abstract class AbstractServer
{
    private readonly string _host;
    private readonly int _port;
    private TcpListener? _server;
    protected readonly IService Service;

    public AbstractServer(string host, int port, IService service)
    {
        _host = host;
        _port = port;
        Service = service;
        _server = null;
    }

    public void StartServer()
    {
        try
        {
            IPAddress address = IPAddress.Parse(_host);
            _server = new TcpListener(address, _port);
            _server.Start();
            while (true)
            {
                Console.WriteLine("Waiting for clients");
                TcpClient client = _server.AcceptTcpClient();
                Console.WriteLine("We have a new client");
                ProcessClient(client);
            }
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (SocketException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    protected abstract void ProcessClient(TcpClient client);
}