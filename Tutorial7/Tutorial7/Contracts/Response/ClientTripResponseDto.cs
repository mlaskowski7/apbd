using Tutorial7.Models;

namespace Tutorial7.Contracts.Response;

public class ClientTripResponseDto
{
    public int TripId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime StartDateTime { get; init; }
    public DateTime EndDateTime { get; init; }
    public int MaxNumberOfParticipants { get; init; }
    public DateTime RegisteredAt { get; init; }
    public DateTime? PaymentDate { get; init; }

    public ClientTripResponseDto(Trip trip, ClientTrip clientTrip)
    {
        TripId = trip.IdTrip;
        Name = trip.Name;
        Description = trip.Description;
        StartDateTime = trip.DateFrom;
        EndDateTime = trip.DateTo;
        MaxNumberOfParticipants = trip.MaxPeople;
        RegisteredAt = DateTimeOffset.FromUnixTimeSeconds(clientTrip.RegisteredAt).DateTime;
        PaymentDate = clientTrip.PaymentDate.HasValue
            ? DateTimeOffset.FromUnixTimeSeconds(clientTrip.PaymentDate.Value).DateTime
            : null;
    }
}
