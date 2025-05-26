using Tutorial9.Application.Contracts.Response;
using Tutorial9.Application.Mappers;
using Tutorial9.Application.Repositories;
using Tutorial9.Domain.Models;

namespace Tutorial9.Application.Services.Impl;

public class TripService(
    ITripRepository tripRepository, 
    ITripMapper tripMapper) : ITripService
{
    public async Task<List<TripResponseDto>> GetAllTripsAsync(CancellationToken cancellationToken = default)
    {
        var trips = await tripRepository.FindAllTripsAsync(cancellationToken);
        return trips.Select(tripMapper.MapEntityToResponse)
                    .ToList();
    }

    public async Task<PaginatedList<TripResponseDto>> GetTripsPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var trips = await tripRepository.FindTripsPaginatedAsync(pageNumber, pageSize, cancellationToken);

        return new PaginatedList<TripResponseDto>()
        {
            PageNumber = trips.PageNumber,
            PageSize = trips.PageSize,
            TotalPages = trips.TotalPages,
            Items = trips.Items.Select(tripMapper.MapEntityToResponse).ToList()
        };
    }
}