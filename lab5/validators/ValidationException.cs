using System;

namespace lab5.validators;

public class ValidationException : Exception
{
    public ValidationException(string? message) : base(message)
    {
    }
}