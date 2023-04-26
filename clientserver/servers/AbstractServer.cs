using System.Net;
using System.Net.Sockets;
using Servicies;

namespace clientserver.servers;

public abstract class AbstractServer
{
    private TcpListener? _server;
    protected readonly IService Service;

    protected AbstractServer(IService service)
    {
        this._server = null;
        this.Service = service;
    }

    public void Start()
    {
        int port = 10102;
        try
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            this._server = new TcpListener(address, port);
            _server.Start();
            while (true)
            {
                Console.WriteLine("Waiting for clients");
                TcpClient client = _server.AcceptTcpClient();
                Console.WriteLine("We got a new client");
                ProcessClient(client);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            StopServer();
        }
    }

    protected abstract void ProcessClient(TcpClient client);

    private void StopServer()
    {
        Console.WriteLine("Shutting server down");
        this._server?.Stop();
    }
}