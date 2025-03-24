namespace tutorial2.Exceptions;

public class DangerousOperationException : Exception
{
    public DangerousOperationException()
    {
    }

    public DangerousOperationException(string message)
        : base(message)
    {
    }
}