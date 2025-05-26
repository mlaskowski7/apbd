using System.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Tutorial9.Application.Repositories;
using Tutorial9.Application.Utils;
using Tutorial9.Domain.Models;
using Tutorial9.Infrastructure.Utils;

namespace Tutorial9.Infrastructure.Repositories.Impl;

public class ClientRepository(TripsDatabaseContext dbContext) : IClientRepository
{
    private readonly DbSet<Client> _clientsDbSet = dbContext.Clients;
    
    public async Task<(Client?, Error?)> FindClientByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbOperationsUtils.TryAsync(async () =>
        {
            return await _clientsDbSet.Include(c => c.ClientTrips)
                                      .Where(c => c.IdClient == id)
                                      .FirstOrDefaultAsync(cancellationToken);
        });
    }

    public async Task<Error?> DeleteClientByIdAsync(Client client, CancellationToken cancellationToken = default)
    {
        return await DbOperationsUtils.TryAsync(async () =>
        {
            _clientsDbSet.Remove(client);
            await dbContext.SaveChangesAsync(cancellationToken);
        });
    }

    public async Task<(Client?, Error?)> FindClientByPeselAsync(string pesel, CancellationToken cancellationToken = default)
    {
        return await DbOperationsUtils.TryAsync(async () =>
        {
            return await _clientsDbSet.Where(c => c.Pesel == pesel)
                                      .FirstOrDefaultAsync(cancellationToken);
        });
    }

    public async Task<Error?> CreateClientAsync(Client client, CancellationToken cancellationToken = default)
    {
        return await DbOperationsUtils.TryAsync(async () =>
        {
            _clientsDbSet.Add(client);
            await dbContext.SaveChangesAsync(cancellationToken);
        });
    }
}