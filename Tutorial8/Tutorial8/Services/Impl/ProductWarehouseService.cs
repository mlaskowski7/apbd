using Tutorial8.Contracts.Request;
using Tutorial8.Entities;
using Tutorial8.Exceptions;
using Tutorial8.Repositories;
using Tutorial8.Utils;

namespace Tutorial8.Services.Impl;

public class ProductWarehouseService : IProductWarehouseService
{
    
    private readonly IProductRepository _productRepository;
    
    private readonly IWarehouseRepository _warehouseRepository;
    
    private readonly IOrderRepository _orderRepository;
    
    private readonly IProductWarehouseRepository _productWarehouseRepository;
    
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public ProductWarehouseService(
        IProductRepository productRepository, 
        IWarehouseRepository warehouseRepository, 
        IOrderRepository orderRepository, 
        IProductWarehouseRepository productWarehouseRepository, 
        IDateTimeProvider dateTimeProvider)
    {
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _orderRepository = orderRepository;
        _productWarehouseRepository = productWarehouseRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<int> AddProductWarehouseAsync(AddProductWarehouseRequestDto dto)
    {
        var (product, warehouse) = await GetProductAndWarehouseByIds(dto.IdProduct, dto.IdWarehouse);

        var purchaseOrder = await _orderRepository.GetPurchaseOrderAsync(dto.IdProduct, dto.Amount, dto.CreatedAt);
        if (purchaseOrder == null)
        {
            throw new NotFoundException($"There is no previously created order for {dto.Amount}x product with id = {dto.IdProduct}");
        }
        
        var presentProductWarehouseId = await _productWarehouseRepository.GetProductWarehouseIdOfCompletedOrderAsync(dto.IdWarehouse);
        if (presentProductWarehouseId != null)
        {
            throw new ConflictException($"Order for {dto.Amount}x product with id {dto.IdProduct} already was completed within product warehouse with id = {presentProductWarehouseId}");
        }
        
        var productWarehouse = new ProductWarehouse()
        {
            Amount = dto.Amount,
            CreatedAt = _dateTimeProvider.Now,
            Order = purchaseOrder,
            Product = product,
            Warehouse = warehouse,
            Price = product.Price * dto.Amount,
        };
        return await _productWarehouseRepository.SaveProductWarehouseAsync(productWarehouse);
    }

    private async Task<(Product, Warehouse)> GetProductAndWarehouseByIds(int productId, int warehouseId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product == null)
        {
            throw new NotFoundException($"Product with id {productId} does not exist");
        }

        var warehouse = await _warehouseRepository.GetWarehouseById(warehouseId);
        if (warehouse == null)
        {
            throw new NotFoundException($"Warehouse with id {warehouseId} does not exist");
        }
        
        return (product, warehouse);
    }
}