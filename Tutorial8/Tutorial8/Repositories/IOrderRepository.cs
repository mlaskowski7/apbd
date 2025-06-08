using Tutorial8.Entities;

namespace Tutorial8.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetPurchaseOrderAsync(int productId, int amount, DateTime createdAt);
}