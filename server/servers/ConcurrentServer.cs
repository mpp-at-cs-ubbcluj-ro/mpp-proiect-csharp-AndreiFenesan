using System.Net.Sockets;
using networking;
using servicies;

namespace server.servers;

public class ConcurrentServer : AbstractServer
{
    public ConcurrentServer(string host, int port, IService service) : base(host, port, service)
    {
    }

    protected override void ProcessClient(TcpClient client)
    {
        ClientWorker worker = new ClientWorker(client, base.Service);
        Thread thread = new Thread(worker.Run);
        thread.Start();
    }
}