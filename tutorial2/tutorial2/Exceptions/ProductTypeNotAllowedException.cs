namespace tutorial2.Exceptions;

public class ProductTypeNotAllowedException : Exception
{
    public ProductTypeNotAllowedException()
    {
    }

    public ProductTypeNotAllowedException(string message)
        : base(message)
    {
    }
}