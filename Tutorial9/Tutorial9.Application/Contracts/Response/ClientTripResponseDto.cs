namespace Tutorial9.Application.Contracts.Response;

public record ClientTripResponseDto(
    ClientResponseDto Client,
    int TripId,
    DateTime RegisteredAt,
    DateTime? PaymentDate);