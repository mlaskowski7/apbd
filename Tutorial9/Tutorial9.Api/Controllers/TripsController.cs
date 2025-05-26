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
        if (pageNumber.HasValue && pageSize.HasValue)
        {
            var paginatedTrips = await tripService.GetTripsPaginatedAsync(pageNumber.Value, pageSize.Value, cancellationToken);
            return Ok(paginatedTrips);
        }

        var trips = await tripService.GetAllTripsAsync(cancellationToken);
        return Ok(trips);
    }
    
}