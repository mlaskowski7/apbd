using Microsoft.Data.SqlClient;
using Tutorial8.Entities;

namespace Tutorial8.Repositories.Impl;

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection") 
                            ?? throw new ArgumentException("No connection string provided");
    }


    public async Task<Product?> GetProductByIdAsync(int id)
    {
        const string sql = """
                           SELECT p.IdProduct, p.Name, p.Description, p.Price
                           FROM Product p
                           WHERE p.IdProduct = @id
                           """;
        
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        
        command.Parameters.AddWithValue("@id", id);
        await using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Product()
        {
            Id = Convert.ToInt32(reader["IdProduct"]),
            Name = Convert.ToString(reader["Name"])!,
            Description = Convert.ToString(reader["Description"])!,
            Price = Convert.ToDecimal(reader["Price"])
        };
    }
}