namespace VetClinicShelterApi.Utils;

public class ResultWrapper<T>
{
    public ResultWrapper(T result)
    {
        IsOk = true;
        Result = result;
    }

    public ResultWrapper(string errorMessage)
    {
        IsOk = false;
        ErrorMessage = errorMessage;
    }
    
    public bool IsOk { get; set; }
    
    public T? Result { get; set; }
    
    public string? ErrorMessage { get; set; }
}