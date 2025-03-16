namespace tutorial2.Exceptions;

public class TemperatureNotMatchingException : Exception
{
    public TemperatureNotMatchingException()
    {
    }

    public TemperatureNotMatchingException(string message)
        : base(message)
    {
    }
}