using Tutorial8.Contracts.Request;

namespace Tutorial8.Services;

public interface IProductWarehouseService
{
    Task<int> AddProductWarehouseAsync(AddProductWarehouseRequestDto dto);
    
    Task<int> AddProductWarehouseUsingStoredProcedureAsync(AddProductWarehouseRequestDto dto);
}