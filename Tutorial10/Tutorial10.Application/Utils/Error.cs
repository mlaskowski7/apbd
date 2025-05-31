namespace Tutorial10.Application.Utils;

public class Error
{
   private Error(string message, ErrorType errorType)
   {
      Message = message;
      Type = errorType;
   } 
   
   public string Message { get; private init; }
   
   public ErrorType Type { get; private init; }
   
   public static Error NotFound(string message) => new(message, ErrorType.NotFound);
   
   public static Error Conflict(string message) => new(message, ErrorType.Conflict);
}