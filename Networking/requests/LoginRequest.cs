namespace Networking.requests;
[Serializable]
public class LoginRequest:IRequest
{
    public string Username { get; }
    public string Password { get; }

    public LoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}