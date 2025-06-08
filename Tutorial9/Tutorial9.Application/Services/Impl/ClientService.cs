using System.Net;
using Tutorial9.Application.Repositories;
using Tutorial9.Application.Utils;

namespace Tutorial9.Application.Services.Impl;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task<Error?> DeleteClientByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var (client, err) = await clientRepository.FindClientByIdAsync(id, cancellationToken);
        
        switch (client, err)
        {
            case (_, { } e):
                return e;
            case (null, null):
                return new Error($"Client with id {id} not found", HttpStatusCode.NotFound);
            case ({ ClientTrips.Count: > 0 }, null):
                return new Error($"Client with id {id} is registered for some trips", HttpStatusCode.Conflict);
            default:
                await clientRepository.DeleteClientByIdAsync(client!, cancellationToken);
                return null;
        }
    }
}