using Networking.requests;

namespace Networking.responses;

[Serializable]
public class ErrorResponse : IResponse
{
    public string Message { get; }

    public ErrorResponse(string message)
    {
        this.Message = message;
    }
}