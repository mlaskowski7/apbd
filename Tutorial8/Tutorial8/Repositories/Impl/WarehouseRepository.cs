using Microsoft.Data.SqlClient;
using Tutorial8.Entities;

namespace Tutorial8.Repositories.Impl;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly string _connectionString;

    public WarehouseRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection") 
                            ?? throw new ArgumentException("No connection string found");
    }
    
    public async Task<Warehouse?> GetWarehouseById(int id)
    {
        const string sql = """
                           SELECT IdWarehouse, Name, Address
                           FROM Warehouse
                           WHERE IdWarehouse = @IdWarehouse
                           """;
        
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        
        command.Parameters.AddWithValue("@IdWarehouse", id);
        
        await using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Warehouse()
        {
            Id = Convert.ToInt32(reader["IdWarehouse"]),
            Name = Convert.ToString(reader["Name"])!,
            Address = Convert.ToString(reader["Address"])!,
        };
    }
}