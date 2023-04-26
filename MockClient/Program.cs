// See https://aka.ms/new-console-template for more information

using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Networking.requests;
using Networking.responses;

namespace MockClient
{
}

public class MockClientApp
{
    private static EventWaitHandle _waitHandle;
    [Obsolete("Obsolete")]
    public static void Main()
    {
        MockClientApp._waitHandle = new AutoResetEvent(true);
        // TcpClient client = new TcpClient("127.0.0.1", 10102);
        // IFormatter formatter = new BinaryFormatter();
        // formatter.Serialize(client.GetStream(),new GetAllCharityCaseRequest());
        // CharityCaseResponse response =(CharityCaseResponse) formatter.Deserialize(client.GetStream());
        // foreach (var charityCaseDto in response.Cases)
        // {
        //     Console.WriteLine(charityCaseDto);
        // }

        MockClientApp._waitHandle = new AutoResetEvent(false);
        Thread thread = new Thread(MockClientApp.Run);
        thread.Start();
        Console.WriteLine("Waiting");
        Thread.Sleep(3000);
        Console.WriteLine("Done waiting");
        _waitHandle.Set();
        while (true)
        {
            
        }


        // formatter.Serialize(client.GetStream(),new LoginRequest("serPop","1234"));
        // IResponse response =(IResponse) formatter.Deserialize(client.GetStream());
        // if (response is ResponseOk ok)
        // {
        //     Console.WriteLine("Login success");
        // }
        // else if (response is ErrorResponse errorResponse)
        // {
        //     Console.WriteLine(errorResponse.Message);
        // }


        // formatter.Serialize(client.GetStream(),new GetDonorWithNameLikeRequest("cu"));
        // IResponse response = (IResponse)formatter.Deserialize(client.GetStream());
        // if (response is GetDonorsWithNameLikeResponse likeResponse)
        // {
        //     foreach (var likeResponseDonor in likeResponse.Donors)
        //     {
        //         Console.WriteLine(likeResponseDonor);
        //     }
        // }

        // IRequest request = new NewDonationRequest(1, "Mihai Rus", "RusMihai@gmail.com", "0741745198", 100);
        // formatter.Serialize(client.GetStream(), request);
        // IResponse response2 = (IResponse)formatter.Deserialize(client.GetStream());
        // if (response2 is ResponseOk)
        // {
        //     Console.WriteLine("OK");
        // }
        // else if (response2 is ErrorResponse errorResponse)
        // {
        //     Console.WriteLine(errorResponse.Message);
        // }
        // formatter.Serialize(client.GetStream());

        // formatter.Serialize(client.GetStream(),new LogOutRequest());
        // client.Close();
        // while (true)
        // {
        //     
        // }
    }

    private static void Run()
    {
        Console.WriteLine("I am in new thread");
        _waitHandle.WaitOne();
        Console.WriteLine("Hello from run method");
    }
}