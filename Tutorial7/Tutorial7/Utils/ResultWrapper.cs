using System.Net;

namespace Tutorial7.Utils;

public class ResultWrapper<T> : ResultWrapperBase
{
    public static ResultWrapper<T> Ok(T result) => new (result);
    
    public static ResultWrapper<T> Err(string message, int responseStatusCode) => new (message, responseStatusCode);
    
    public static ResultWrapper<T> FromErr(ResultWrapperBase innerResultWrapper)
    {
        if (innerResultWrapper.IsOk)
        {
            throw new ArgumentException(nameof(innerResultWrapper));
        }

        return new ResultWrapper<T>(innerResultWrapper.Error!);
    }
        
    private ResultWrapper(T result)
    {
        IsOk = true;
        Result = result;
    }

    private ResultWrapper(string errorMessage, int responseStatusCode)
    {
        IsOk = false;
        Error = new ErrorWrapper(errorMessage, responseStatusCode);
    }
    
    private ResultWrapper(ErrorWrapper error)
    {
        IsOk = false;
        Error = error;
    }
    
    public T? Result { get; set; }
    
}