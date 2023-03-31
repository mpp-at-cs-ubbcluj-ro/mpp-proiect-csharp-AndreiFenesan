namespace Teledon.validators;

public class ValidationException : Exception
{
    public ValidationException(string? message) : base(message)
    {
    }
}