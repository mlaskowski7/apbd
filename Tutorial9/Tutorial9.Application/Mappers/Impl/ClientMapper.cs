using Tutorial9.Application.Contracts.Response;
using Tutorial9.Domain.Models;

namespace Tutorial9.Application.Mappers.Impl;

public class ClientMapper : IClientMapper
{
    public ClientResponseDto MapEntityToResponse(Client client)
    {
        return new ClientResponseDto(client.FirstName, client.LastName);
    }
}