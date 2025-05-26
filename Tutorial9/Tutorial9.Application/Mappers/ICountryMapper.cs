using Tutorial9.Application.Contracts.Response;
using Tutorial9.Domain.Models;

namespace Tutorial9.Application.Mappers;

public interface ICountryMapper : IMapper<Country, CountryResponseDto>
{
}