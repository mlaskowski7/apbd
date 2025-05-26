using Tutorial9.Application.Contracts.Response;
using Tutorial9.Domain.Models;

namespace Tutorial9.Application.Mappers.Impl;

public class CountryMapper : ICountryMapper
{
    public CountryResponseDto MapEntityToResponse(Country country)
    {
        return new CountryResponseDto(country.Name);
    }
}