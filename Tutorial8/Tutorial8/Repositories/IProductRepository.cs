using Tutorial8.Entities;

namespace Tutorial8.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(int id);
}