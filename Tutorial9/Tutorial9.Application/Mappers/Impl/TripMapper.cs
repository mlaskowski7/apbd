using Tutorial9.Application.Contracts.Response;
using Tutorial9.Domain.Models;

namespace Tutorial9.Application.Mappers.Impl;

public class TripMapper(
    IClientMapper clientMapper, 
    ICountryMapper countryMapper) : ITripMapper
{
    public TripResponseDto MapEntityToResponse(Trip trip)
    {
        var clients = trip.ClientTrips.Select(ct => clientMapper.MapEntityToResponse(ct.IdClientNavigation))
                                      .ToList();
        var countries = trip.IdCountries.Select(countryMapper.MapEntityToResponse)
                                        .ToList();
        return new TripResponseDto(
            trip.Name, 
            trip.Description, 
            trip.DateFrom, 
            trip.DateTo, 
            trip.MaxPeople, 
            countries,
            clients);
    }
}