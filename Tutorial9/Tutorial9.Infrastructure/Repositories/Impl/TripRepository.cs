using Microsoft.EntityFrameworkCore;
using Tutorial9.Application.Repositories;
using Tutorial9.Application.Utils;
using Tutorial9.Domain.Models;
using Tutorial9.Infrastructure.Utils;

namespace Tutorial9.Infrastructure.Repositories.Impl;

public class TripRepository(TripsDatabaseContext tripsDbContext) : ITripRepository
{
    private readonly DbSet<Trip> _tripsDbSet = tripsDbContext.Trips;

    public async Task<List<Trip>> FindAllTripsAsync(CancellationToken cancellationToken = default)
    {
        return await _tripsDbSet.Include(t => t.ClientTrips)
                                .ThenInclude(ct => ct.IdClientNavigation)
                                .Include(t => t.IdCountries)
                                .OrderByDescending(t => t.DateFrom)
                                .ToListAsync(cancellationToken);
    }

    public async Task<PaginatedList<Trip>> FindTripsPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var tripsCount = await _tripsDbSet.Include(t => t.IdCountries)
                                          .Include(t => t.ClientTrips)
                                          .ThenInclude(ct => ct.IdClientNavigation)
                                          .OrderByDescending(t => t.DateFrom)
                                          .CountAsync(cancellationToken);
        
        var totalPages = tripsCount / pageSize;
        var requestedTrips = await _tripsDbSet.Include(t => t.ClientTrips)
                                              .ThenInclude(ct => ct.IdClientNavigation)
                                              .Include(t => t.IdCountries)
                                              .OrderByDescending(t => t.DateFrom)
                                              .Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToListAsync(cancellationToken);

        return new PaginatedList<Trip>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages + 1,
            Items = requestedTrips
        };
    }

    public async Task<(Trip?, Error?)> FindTripByIdAsync(int tripId, CancellationToken cancellationToken = default)
    {
        return await DbOperationsUtils.TryAsync(async () =>
        {
            return await _tripsDbSet.Where(t => t.IdTrip == tripId)
                                    .FirstOrDefaultAsync(cancellationToken);
        });
    }
}