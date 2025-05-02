namespace Tutorial7.Models;

public class ClientTrip
{
    public required int IdClient { get; set; }
    
    public required int IdTrip { get; set; }
    
    public required int RegisteredAt { get; set; }
    
    public int? PaymentDate { get; set; }
}