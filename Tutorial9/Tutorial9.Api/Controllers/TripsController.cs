using Microsoft.AspNetCore.Mvc;
using Tutorial9.Application.Contracts.Request;
using Tutorial9.Application.Contracts.Response;
using Tutorial9.Application.Services;
using Tutorial9.Domain.Models;

namespace Tutorial9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController(ITripService tripService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<TripResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PaginatedList<TripResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTripsDescByStartDate(
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken = default)
    {
        return (pageNumber, pageSize) switch
        {
            ({ } page, { } size) => Ok(await tripService.GetTripsPaginatedAsync(page, size, cancellationToken)),
            ({ } page, null) => Ok(await tripService.GetTripsPaginatedAsync(page, 10, cancellationToken)),
            _ => Ok(await tripService.GetAllTripsAsync(cancellationToken))
        };
    }

    [HttpPost("{tripId:int}/clients")]
    [ProducesResponseType(typeof(ClientTripResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AssignClientToTrip(
        [FromRoute] int tripId,
        [FromBody] AssignClientToTripRequestDto assignClientToTripDto, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var (clientTrip, err) = await tripService.CreateClientForTripAsync(assignClientToTripDto, tripId, cancellationToken);

        if (err is not null)
        {
            return StatusCode((int)err.StatusCode, err);
        }
        
        return Created($"Client was created and assigned to the trip", clientTrip);
    }
    
}