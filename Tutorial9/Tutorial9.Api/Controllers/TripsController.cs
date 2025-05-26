using Microsoft.AspNetCore.Mvc;
using Tutorial9.Application.Contracts.Response;
using Tutorial9.Application.Services;
using Tutorial9.Domain.Models;

namespace Tutorial9.Controllers;

[ApiController]
[Route("/api/[controller]")]
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
    
}