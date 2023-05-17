using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using model;
using servicies;

namespace networking;

public class ClientWorker : IObserver
{
    private readonly TcpClient _client;
    private readonly IService _service;
    private volatile bool _connected;

    public ClientWorker(TcpClient client, IService service)
    {
        _client = client;
        _service = service;
        this._connected = true;
        this._service.AddObserver(this);
    }


    public void Run()
    {
        Console.WriteLine("Worker Started");
        using (_client)
        {
            using (NetworkStream stream = this._client.GetStream())
            {
                stream.Flush();
                while (_connected)
                {
                    try
                    {
                        string jsonRequest = ReadString(_client.GetStream());
                        string response = ProcessJsonRequest(jsonRequest);
                        WriteString(_client.GetStream(), response);
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

    private string ProcessJsonRequest(string jsonRequest)
    {
        JsonDocument data = JsonDocument.Parse(jsonRequest);
        JsonElement root = data.RootElement;
        var type = root.GetProperty("type");
        int int32Type = type.GetInt32();
        if (int32Type == 1)
        {
            return ProcessLoginRequest(root);
        }

        if (int32Type == 2)
        {
            return ProcessGetAllCharityCasesDto(root);
        }

        if (int32Type == 3)
        {
            return ProcessGetDonors(root);
        }

        if (int32Type == 4)
        {
            return ProcessNewDonation(root);
        }

        return "as";
    }

    private string ProcessNewDonation(JsonElement root)
    {
        long caseId = root.GetProperty("caseId").GetInt64();
        string donorName = root.GetProperty("donorName").GetString();
        string email = root.GetProperty("email").GetString();
        string phone = root.GetProperty("phone").GetString();
        double donationAmount = root.GetProperty("amount").GetDouble();
        JsonObject jsonObject = new JsonObject();
        try
        {
            _service.AddNewDonation(caseId, donorName, email, phone, donationAmount);
            jsonObject.Add("response", "OK");
        }
        catch (ServiceException e)
        {
            jsonObject.Add("response", "Error");
            jsonObject.Add("message", e.Message);
        }
        catch (ValidationException e)
        {
            jsonObject.Add("response", "Error");
            jsonObject.Add("message", e.Message);
        }

        return jsonObject.ToString();
    }

    private string ProcessGetDonors(JsonElement root)
    {
        string nameContaining = root.GetProperty("nameContaining").ToString();
        IEnumerator<Donor> donors = _service.GetDonorsWithNameContaining(nameContaining);
        List<Donor> donorsInList = new List<Donor>();
        while (donors.MoveNext())
        {
            donorsInList.Add(donors.Current);
        }

        return JsonSerializer.Serialize(donorsInList);
    }

    private string ProcessGetAllCharityCasesDto(JsonElement root)
    {
        JsonObject jsonObject = new JsonObject();
        List<CharityCaseDto> charityCaseDtos = _service.GetAllCharityCases();
        string jsonResponse = JsonSerializer.Serialize(charityCaseDtos);
        return jsonResponse;
    }

    private string ProcessLoginRequest(JsonElement root)
    {
        string? username = root.GetProperty("username").GetString();
        string? password = root.GetProperty("password").GetString();
        if (username != null && password != null)
        {
            bool isUserValid = _service.AuthenticateVolunteer(username, password);
            if (isUserValid)
            {
                JsonObject obj = new JsonObject();
                obj.Add("response", "OK");
                return obj.ToString();
            }
        }

        JsonObject error = new JsonObject();
        error.Add("response", "Error");
        return error.ToString();
    }

    public void UpdateTeledonEvent()
    {
        JsonObject jsonObject = new JsonObject();
        jsonObject.Add("response", "update");
        WriteString(_client.GetStream(), jsonObject.ToString());
    }

    private string ReadString(NetworkStream stream)
    {
        byte[] buffer = new byte[4];
        int count = stream.Read(buffer, 0, 4);
        int length = BitConverter.ToInt32(buffer, 0);
        Console.WriteLine($"Len {length}");
        byte[] msgBuffer = new byte[length];
        int no = stream.Read(msgBuffer, 0, length);

        String message = UTF8Encoding.ASCII.GetString(msgBuffer, 0, length);
        Console.WriteLine($"Got a request: {message}");
        return message;
    }

    private void WriteString(NetworkStream stream, string str)
    {
        Console.WriteLine($"Sending response {str}");
        var buffer = BitConverter.GetBytes(str.Length);
        Array.Reverse(buffer);
        stream.Write(buffer);
        byte[] strData = Encoding.ASCII.GetBytes(str);
        stream.Write(strData);
    }
}