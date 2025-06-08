using Tutorial9.Application.Utils;
using Tutorial9.Domain.Models;

namespace Tutorial9.Application.Repositories;

public interface ITripRepository
{
    Task<List<Trip>> FindAllTripsAsync(CancellationToken cancellationToken = default);
    
    Task<PaginatedList<Trip>> FindTripsPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    
    Task<(Trip?, Error?)> FindTripByIdAsync(int tripId, CancellationToken cancellationToken = default);
}