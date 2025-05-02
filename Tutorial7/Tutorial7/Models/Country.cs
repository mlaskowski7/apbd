namespace Tutorial7.Models;

public class Country
{
    public required int IdCountry { get; set; }
    
    public required string Name { get; set; }
    
    public List<Trip> Trips { get; set; } = new List<Trip>();
}