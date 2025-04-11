namespace VetClinicShelterApi.Utils;

public class ResultWrapper<T>
{

    public static ResultWrapper<T> Ok(T result) => new (result);
    
    public static ResultWrapper<T> Err(string message) => new (message);
        
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
    
    public bool IsOk { get; set; }
    
    public T? Result { get; set; }
    
    public string? ErrorMessage { get; set; }
}