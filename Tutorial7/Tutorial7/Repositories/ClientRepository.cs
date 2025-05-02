using System.Collections;
using Microsoft.Data.SqlClient;
using Tutorial7.Models;
using Tutorial7.Utils;

namespace Tutorial7.Repositories;

public class ClientRepository : IClientRepository
{
    
    private readonly string _connectionString;

    public ClientRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string is missing");
        }

        _connectionString = connectionString;
    }
    public async Task<ResultWrapper<IEnumerable<Trip>>> GetClientTripsAsync(int id)
    {
        var result = await DataAccessUtils.TryExecuteAsync<IEnumerable<Trip>>(async () =>
        {
            var clientTrips = new Dictionary<int, Trip>();

            var query = @"
            SELECT 
                t.IdTrip,
                t.Name,
                t.Description,
                t.DateFrom,
                t.DateTo,
                t.MaxPeople,
                ct.IdClient,
                ct.RegisteredAt,
                ct.PaymentDate
            FROM Trip t
            INNER JOIN Client_Trip ct ON t.IdTrip = ct.IdTrip
            WHERE ct.IdClient = @ClientId";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ClientId", id);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int idTrip = (int)reader["IdTrip"];

                        if (!clientTrips.ContainsKey(idTrip))
                        {
                            clientTrips[idTrip] = new Trip
                            {
                                IdTrip = idTrip,
                                Name = (string)reader["Name"],
                                Description = (string)reader["Description"],
                                DateFrom = (DateTime)reader["DateFrom"],
                                DateTo = (DateTime)reader["DateTo"],
                                MaxPeople = (int)reader["MaxPeople"],
                                ClientTrips = new List<ClientTrip>()
                            };
                        }

                        clientTrips[idTrip].ClientTrips.Add(new ClientTrip
                        {
                            IdTrip = idTrip,
                            IdClient = (int)reader["IdClient"],
                            RegisteredAt = (int)reader["RegisteredAt"],
                            PaymentDate = (int)reader["PaymentDate"]
                        });
                    }
                }
            }

            return clientTrips.Values.ToList();
        });

        if (result.IsOk && !result.Result!.Any())
        {
            return ResultWrapper<IEnumerable<Trip>>.Err($"No trips found for client with ID {id}.", 404);
        }

        return result;
    }
}