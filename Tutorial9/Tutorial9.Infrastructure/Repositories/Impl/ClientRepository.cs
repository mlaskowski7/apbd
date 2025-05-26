using System.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Tutorial9.Application.Repositories;
using Tutorial9.Application.Utils;
using Tutorial9.Domain.Models;

namespace Tutorial9.Infrastructure.Repositories.Impl;

public class ClientRepository(TripsDatabaseContext dbContext) : IClientRepository
{
    private readonly DbSet<Client> _clientsDbSet = dbContext.Clients;
    
    public async Task<(Client?, Error?)> FindClientByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = await _clientsDbSet.Include(c => c.ClientTrips)
                .Where(c => c.IdClient == id)
                .FirstOrDefaultAsync(cancellationToken);

            return (client, null);
        }
        catch (SqlException)
        {
            return (null, new Error($"Unexpected exception occurred during db access", HttpStatusCode.InternalServerError));
        }
        
    }

    public async Task<Error?> DeleteClientByIdAsync(Client client, CancellationToken cancellationToken = default)
    {
        try
        {
            _clientsDbSet.Remove(client);
            await dbContext.SaveChangesAsync(cancellationToken);
            return null;
        }
        catch (SqlException)
        {
            return new Error($"Unexpected exception occurred during db access", HttpStatusCode.InternalServerError);
        }
    }
}