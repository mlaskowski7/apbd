using Tutorial9.Application.Utils;
using Tutorial9.Domain.Models;

namespace Tutorial9.Application.Repositories;

public interface IClientRepository
{
    Task<(Client?, Error?)> FindClientByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<Error?> DeleteClientByIdAsync(Client client, CancellationToken cancellationToken = default);
}