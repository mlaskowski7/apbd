namespace Tutorial7.Models;

public class Client
{
    public required int IdClient { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
    
    public required string Telephone { get; set; }
    
    public required string Pesel { get; set; }
    
    public IEnumerable<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
}