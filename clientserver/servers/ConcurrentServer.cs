using System.Net.Sockets;
using Networking;
using Servicies;

namespace clientserver.servers;

public class ConcurrentServer: AbstractServer
{
    public ConcurrentServer(IService service) : base(service)
    {
    }

    protected override void ProcessClient(TcpClient client)
    {
        ClientWorker worker = new ClientWorker(client, base.Service);
        base.Service.AddObserver(worker);
        Thread thread = new Thread(worker.Run);
        thread.Start();
    }
}