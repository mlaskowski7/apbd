namespace VetClinicShelterApi.Utils;

public class ResultWrapper<T> : ResultWrapperBase
{

    public static ResultWrapper<T> Ok(T result) => new (result);
    
    public static ResultWrapper<T> Err(string message) => new (message);

    public static ResultWrapper<T> FromErr(ResultWrapperBase innerResultWrapper)
    {
        if (innerResultWrapper.IsOk)
        {
            throw new ArgumentException(nameof(innerResultWrapper));
        }

        return new ResultWrapper<T>(innerResultWrapper.ErrorMessage 
            ?? "Some error occured, couldnt extract the message from inner result, should never occur");
    }
        
    private ResultWrapper(T result)
    {
        IsOk = true;
        Result = result;
    }

    private ResultWrapper(string errorMessage)
    {
        IsOk = false;
        ErrorMessage = errorMessage;
    }
    
    public T? Result { get; set; }
    
}