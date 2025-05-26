using Tutorial9.Application.Contracts.Request;
using Tutorial9.Application.Contracts.Response;
using Tutorial9.Application.Utils;
using Tutorial9.Domain.Models;

namespace Tutorial9.Application.Services;

public interface ITripService
{
    Task<List<TripResponseDto>> GetAllTripsAsync(CancellationToken cancellationToken = default);

    Task<PaginatedList<TripResponseDto>> GetTripsPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    
    Task<(ClientTripResponseDto?, Error?)> CreateClientForTripAsync(AssignClientToTripRequestDto assignClientToTripDto, int tripId, CancellationToken cancellationToken = default);
}