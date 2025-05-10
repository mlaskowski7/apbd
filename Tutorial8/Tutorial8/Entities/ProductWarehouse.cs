namespace Tutorial8.Entities;

public class ProductWarehouse : BaseEntity
{
    public required Warehouse Warehouse { get; set; }
    
    public required Product Product { get; set; }
    
    public required Order Order { get; set; }
    
    public required int Amount { get; set; }
    
    public required decimal Price { get; set; }
    
    public required DateTime CreatedAt { get; set; }
}