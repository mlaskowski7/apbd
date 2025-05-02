using Tutorial7.Contracts.Response;
using Tutorial7.Utils;

namespace Tutorial7.Services;

public interface IClientService
{
    Task<ResultWrapper<IEnumerable<ClientTripResponseDto>>> GetClientTripsAsync(int id);
}