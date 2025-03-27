using System;

namespace LegacyApp.Exceptions;

public class UserCreditNotFoundException : Exception
{
    public UserCreditNotFoundException()
    {
    }

    public UserCreditNotFoundException(string message)
        : base(message)
    {
    }

    public UserCreditNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}