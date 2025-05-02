using Microsoft.AspNetCore.Mvc;
using Tutorial7.Contracts.Response;
using Tutorial7.Models;
using Tutorial7.Services;

namespace Tutorial7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpGet("{id}/trips")]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetTripsAsync([FromRoute] int id)
    {
        var clientTripsResult = await clientService.GetClientTripsAsync(id);
        
        return clientTripsResult.IsOk ? 
            Ok(clientTripsResult.Result) : 
            StatusCode(clientTripsResult.Error!.ResponseStatusCode, clientTripsResult.Error!.Message);
    }
}