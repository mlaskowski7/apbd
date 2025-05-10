using Tutorial8.Entities;

namespace Tutorial8.Repositories;

public interface IProductWarehouseRepository
{
    Task<int?> GetProductWarehouseIdOfCompletedOrderAsync(int orderId);
    
    Task<int> SaveProductWarehouseAsync(ProductWarehouse productWarehouse);
}