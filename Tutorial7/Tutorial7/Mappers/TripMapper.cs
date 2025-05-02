using Tutorial7.Contracts.Response;
using Tutorial7.Models;

namespace Tutorial7.Mappers;

public class TripMapper(CountryMapper countryMapper)
{
    public TripResponseDto MapToResponse(Trip trip)
    {
        var countries = trip.Countries.Select(countryMapper.MapToResponse);
        return new TripResponseDto(
            trip.IdTrip, 
            trip.Name, 
            trip.Description, 
            trip.DateFrom, 
            trip.DateTo, 
            trip.MaxPeople, 
            countries);
    }
}