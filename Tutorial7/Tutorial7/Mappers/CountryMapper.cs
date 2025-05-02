using Tutorial7.Contracts.Response;
using Tutorial7.Models;

namespace Tutorial7.Mappers;

public class CountryMapper
{
    public CountryResponseDto MapToResponse(Country country)
    {
        return new CountryResponseDto(country.IdCountry, country.Name);
    }
}