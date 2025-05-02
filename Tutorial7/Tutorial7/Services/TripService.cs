using Tutorial7.Contracts.Response;
using Tutorial7.Mappers;
using Tutorial7.Repositories;
using Tutorial7.Utils;

namespace Tutorial7.Services;

public class TripService(ITripRepository tripRepository, TripMapper tripMapper) : ITripService
{
    public async Task<ResultWrapper<IEnumerable<TripResponseDto>>> GetAllTrips()
    {
        var tripsResult = await tripRepository.GetAll();
        if (!tripsResult.IsOk)
        {
            return ResultWrapper<IEnumerable<TripResponseDto>>.FromErr(tripsResult);
        }
        var response = tripsResult.Result!.Select(tripMapper.MapToResponse);
        
        return ResultWrapper<IEnumerable<TripResponseDto>>.Ok(response);
    }
}