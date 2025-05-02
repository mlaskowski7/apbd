using System.Collections;
using Tutorial7.Contracts.Request;
using Tutorial7.Contracts.Response;
using Tutorial7.Models;
using Tutorial7.Repositories;
using Tutorial7.Utils;

namespace Tutorial7.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task<ResultWrapper<IEnumerable<ClientTripResponseDto>>> GetClientTripsAsync(int id)
    {
        var clientTripsResult = await clientRepository.GetClientTripsAsync(id);

        if (!clientTripsResult.IsOk)
        {
            return ResultWrapper<IEnumerable<ClientTripResponseDto>>.FromErr(clientTripsResult);
        }
        
        var response = clientTripsResult.Result!
                                                               .SelectMany(trip => 
                                                                   trip.ClientTrips.Select(clientTrip => new ClientTripResponseDto(trip, clientTrip)))
                                                               .ToList();
        
        return ResultWrapper<IEnumerable<ClientTripResponseDto>>.Ok(response);
    }

    public async Task<ResultWrapper<CreateClientResponseDto>> CreateAsync(CreateClientRequestDto createClientRequestDto)
    {
        var createdIdResult = await clientRepository.CreateAsync(
            createClientRequestDto.FirstName, 
            createClientRequestDto.LastName, 
            createClientRequestDto.Email, 
            createClientRequestDto.Telephone, 
            createClientRequestDto.Pesel);

        if (!createdIdResult.IsOk)
        {
            return ResultWrapper<CreateClientResponseDto>.FromErr(createdIdResult);
        }
        
        var response = new CreateClientResponseDto(createdIdResult.Result);
        
        return ResultWrapper<CreateClientResponseDto>.Ok(response);
    }
}