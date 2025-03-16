namespace tutorial2.Exceptions;

public class ShipOverloadException : Exception
{
    public ShipOverloadException()
    {
    }

    public ShipOverloadException(string message)
        : base(message)
    {
    }
}