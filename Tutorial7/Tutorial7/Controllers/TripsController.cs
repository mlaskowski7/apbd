using Microsoft.AspNetCore.Mvc;
using Tutorial7.Contracts.Response;
using Tutorial7.Services;

namespace Tutorial7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController(ITripService tripService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> Get()
    {
        var tripsResult = await tripService.GetAllTrips();

        return tripsResult.IsOk ? 
            Ok(tripsResult.Result) : 
            StatusCode(tripsResult.Error!.ResponseStatusCode, tripsResult.Error!.Message);
    }
}