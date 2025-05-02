using Tutorial7.Contracts.Response;
using Tutorial7.Utils;

namespace Tutorial7.Services;

public interface ITripService
{
    Task<ResultWrapper<IEnumerable<TripResponseDto>>> GetAllTripsAsync();
}