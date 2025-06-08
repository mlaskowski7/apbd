namespace Tutorial8.Entities;

public class Order : BaseEntity
{
    public Product Product { get; set; }
    
    public required int Amount { get; set; }
    
    public required DateTime CreatedAt { get; set; }
    
    public DateTime? FulfilledAt { get; set; }
}