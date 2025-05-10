using Microsoft.Data.SqlClient;
using Tutorial8.Entities;

namespace Tutorial8.Repositories.Impl;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection") 
                            ?? throw new ArgumentException("No connection string provided");
    }
    public async Task<Order?> GetPurchaseOrderAsync(int productId, int amount, DateTime createdAt)
    {
        const string sql = """
                           SELECT TOP 1 o.IdOrder, o.IdProduct, o.Amount, o.CreatedAt, o.FulfilledAt 
                           FROM [Order] o
                           WHERE o.IdProduct = @productId AND o.Amount = @amount AND o.CreatedAt < @createdAt
                           """;
        
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        
        command.Parameters.AddWithValue("@productId", productId);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@createdAt", createdAt);
        
        await using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Order()
        {
            Id = Convert.ToInt32(reader["IdOrder"]),
            Amount = Convert.ToInt32(reader["Amount"]),
            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
            FulfilledAt = reader.IsDBNull(reader.GetOrdinal("FulfilledAt"))
                ? null
                : Convert.ToDateTime(reader["FulfilledAt"]),
        };

    }
}