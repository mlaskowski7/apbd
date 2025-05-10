using Tutorial8.Entities;

namespace Tutorial8.Repositories;

public interface IWarehouseRepository
{
    Task<Warehouse?> GetWarehouseById(int id);
}