namespace Tutorial10.Application.Utils;

public class Result<T>
{
    private readonly T? _value;
    
    private readonly Error? _error;
    
    private Result(T value)
    {
        IsOk = true;
        _value = value;
    }

    private Result(Error error)
    {
        IsOk = false;
        _error = error;
    }
    
    public bool IsOk { get; }
    
    public bool IsError => !IsOk;

    public Error Error
    {
       get
       {
           if (IsOk)
           {
               throw new InvalidOperationException("This result is OK, cannot access the error");
           }
           
           return _error!;
       } 
    }

    public T Value
    {
        get
        {
            if (IsError)
            {
                throw new InvalidOperationException("This result is ERR, cannot access the value");
            }
            
            return _value!;
        }    
    }
    
    public static Result<T> Ok(T value) => new(value);
    
    public static Result<T> Err(Error error) => new(error);
}