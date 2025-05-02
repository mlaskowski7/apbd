using System.Net;
using Microsoft.Data.SqlClient;
using Tutorial7.Models;
using Tutorial7.Utils;

namespace Tutorial7.Repositories;

public class TripRepository : ITripRepository
{

    private readonly string _connectionString;

    public TripRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string is missing");
        }

        _connectionString = connectionString;
    }

    public async Task<ResultWrapper<IEnumerable<Trip>>> GetAllAsync()
    {
        return await DataAccessUtils.TryExecuteAsync<IEnumerable<Trip>>(async () =>
        {
            var trips = new Dictionary<int, Trip>();

            var query = @"
            SELECT 
                t.IdTrip, t.Name, t.Description, t.DateFrom, t.DateTo, t.MaxPeople,
                c.IdCountry, c.Name AS CountryName
            FROM Trip t
            LEFT JOIN Country_Trip ct ON t.IdTrip = ct.IdTrip
            LEFT JOIN Country c ON ct.IdCountry = c.IdCountry";
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int idTrip = (int)reader["IdTrip"];

                        if (!trips.ContainsKey(idTrip))
                        {
                            trips[idTrip] = new Trip()
                            {
                                IdTrip = idTrip,
                                Name = (string)reader["Name"],
                                Description = (string)reader["Description"],
                                DateFrom = (DateTime)reader["DateFrom"],
                                DateTo = (DateTime)reader["DateTo"],
                                MaxPeople = (int)reader["MaxPeople"],
                                Countries = new List<Country>()
                            };
                        }

                        if (reader["IdCountry"] != DBNull.Value)
                        {
                            trips[idTrip].Countries.Add(new Country
                            {
                                IdCountry = (int)reader["IdCountry"],
                                Name = (string)reader["CountryName"]
                            });
                        }
                    }
                }
            }

            return trips.Values.ToList();
        });
    }
}