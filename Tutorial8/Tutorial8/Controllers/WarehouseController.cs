using Microsoft.AspNetCore.Mvc;
using Tutorial8.Contracts.Request;
using Tutorial8.Exceptions;
using Tutorial8.Services;

namespace Tutorial8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IProductWarehouseService _productWarehouseService;

    public WarehouseController(IProductWarehouseService productWarehouseService)
    {
        _productWarehouseService = productWarehouseService;
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddProduct([FromBody] AddProductWarehouseRequestDto warehouseRequestBody)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var id = await _productWarehouseService.AddProductWarehouseAsync(warehouseRequestBody);
            return Ok(id);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
    }
}