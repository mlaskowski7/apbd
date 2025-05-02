namespace Tutorial7.Contracts.Response;

public record TripResponseDto(
    int TripId,
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime EndDateTime,
    int MaxNumberOfParticipants,
    IEnumerable<CountryResponseDto> Countries)
{
    
}