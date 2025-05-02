namespace Tutorial7.Models;

public class Trip
{
    public required int IdTrip { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public required DateTime DateFrom { get; set; }
    
    public required DateTime DateTo { get; set; }
    
    public int MaxPeople { get; set; }
    
    public IEnumerable<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
    
    public IEnumerable<Country> Countries { get; set; } = new List<Country>();
}