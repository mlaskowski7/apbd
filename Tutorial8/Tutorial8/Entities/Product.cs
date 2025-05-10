namespace Tutorial8.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public required decimal Price { get; set; }
}