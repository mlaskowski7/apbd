namespace Tutorial9.Application.Contracts.Response;

public record TripResponseDto(
    string Name,
    string Description,
    DateTime DateFrom,
    DateTime DateTo,
    int MaxPeople,
    List<CountryResponseDto> Countries,
    List<ClientResponseDto> Clients);