using Microsoft.AspNetCore.Mvc;
using Tutorial9.Application.Services;

namespace Tutorial9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteClientByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var err = await clientService.DeleteClientByIdAsync(id, cancellationToken);
        return err switch
        {
            null => NoContent(),
            {} e => StatusCode((int)e.StatusCode, e.Message)
        };
    }
}