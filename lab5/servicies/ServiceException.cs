using System;

namespace lab5.servicies;

public class ServiceException: Exception
{
    public ServiceException(string? message) : base(message)
    {
    }
}