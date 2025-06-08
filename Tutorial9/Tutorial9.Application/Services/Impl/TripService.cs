using System.Net;
using System.Security.Cryptography;
using Tutorial9.Application.Contracts.Request;
using Tutorial9.Application.Contracts.Response;
using Tutorial9.Application.Mappers;
using Tutorial9.Application.Repositories;
using Tutorial9.Application.Utils;
using Tutorial9.Domain.Models;

namespace Tutorial9.Application.Services.Impl;

public class TripService(
    ITripRepository tripRepository, 
    ITripMapper tripMapper,
    IClientRepository clientRepository,
    IDateTimeProvider dateTimeProvider) : ITripService
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

    public async Task<(ClientTripResponseDto?, Error?)> CreateClientForTripAsync(
        AssignClientToTripRequestDto assignClientToTripDto,
        int tripId,
        CancellationToken cancellationToken = default)
    {
        var (clientWithPesel, err) = await clientRepository.FindClientByPeselAsync(assignClientToTripDto.Pesel, cancellationToken);
        switch (clientWithPesel, err)
        {
            case ({ }, null):
                return (null, new Error($"Client with Pesel {assignClientToTripDto.Pesel} already exists.", HttpStatusCode.Conflict));
            case (_, { } e):
                return (null, e);
        }
        
        var (trip, tripErr) = await tripRepository.FindTripByIdAsync(tripId, cancellationToken);
        switch (trip, tripErr)
        {
            case (_, { } e):
                return (null, e);
            case (null, null):
                return (null, new Error($"Trip with id {tripId} was not found.", HttpStatusCode.NotFound));
        }

        if (trip.DateFrom <= dateTimeProvider.Now)
        {
            return (null, new Error($"Requested trip is not in the future", HttpStatusCode.NotFound));
        }

        return await CreateClientTripAsync(trip, assignClientToTripDto, cancellationToken);
    }

    private async Task<(ClientTripResponseDto?, Error?)> CreateClientTripAsync(
        Trip trip, 
        AssignClientToTripRequestDto assignClientToTripDto, 
        CancellationToken cancellationToken = default)
    {
        var client = new Client()
        {
            FirstName = assignClientToTripDto.FirstName,
            LastName = assignClientToTripDto.LastName,
            Email = assignClientToTripDto.Email,
            Telephone = assignClientToTripDto.PhoneNumber,
            Pesel = assignClientToTripDto.Pesel,
        };

        var clientTrip = new ClientTrip()
        {
            IdClientNavigation = client,
            IdTripNavigation = trip,
            PaymentDate = assignClientToTripDto.PaymentDate,
            RegisteredAt = dateTimeProvider.Now,
        };
        
        client.ClientTrips.Add(clientTrip);
        
        var err = await clientRepository.CreateClientAsync(client, cancellationToken);
        if (err != null)
        {
            return (null, err);
        }

        return (new ClientTripResponseDto(
            new ClientResponseDto(client.FirstName, client.LastName), 
            trip.IdTrip,
            clientTrip.RegisteredAt, 
            clientTrip.PaymentDate), null);
    }
}