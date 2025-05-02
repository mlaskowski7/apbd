using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Tutorial7.Contracts.Request;
using Tutorial7.Contracts.Response;
using Tutorial7.Models;
using Tutorial7.Services;

namespace Tutorial7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpGet("{id}/trips")]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetTripsByClientIdAsync([FromRoute] int id)
    {
        var clientTripsResult = await clientService.GetClientTripsAsync(id);
        
        return clientTripsResult.IsOk ? 
            Ok(clientTripsResult.Result) : 
            StatusCode(clientTripsResult.Error!.ResponseStatusCode, clientTripsResult.Error!.Message);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateClientAsync([FromBody] CreateClientRequestDto createClientRequestDto)
    {
        if (!ModelState.IsValid || 
            createClientRequestDto.FirstName.IsNullOrEmpty() || 
            createClientRequestDto.LastName.IsNullOrEmpty() ||
            createClientRequestDto.Email.IsNullOrEmpty() ||
            createClientRequestDto.Telephone.IsNullOrEmpty() ||
            createClientRequestDto.Pesel.IsNullOrEmpty())
        {
            return BadRequest(ModelState);
        }
        
        var createResult = await clientService.CreateAsync(createClientRequestDto);
        
        return createResult.IsOk ?
            Ok(createResult.Result) :
            StatusCode(createResult.Error!.ResponseStatusCode, createResult.Error!.Message);
    }
}