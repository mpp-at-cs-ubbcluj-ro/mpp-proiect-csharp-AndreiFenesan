namespace Teledon.servicies;

public class ServiceException: Exception
{
    public ServiceException(string? message) : base(message)
    {
    }
}