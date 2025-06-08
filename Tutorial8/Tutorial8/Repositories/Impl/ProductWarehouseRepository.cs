using Microsoft.Data.SqlClient;
using Tutorial8.Entities;

namespace Tutorial8.Repositories.Impl;

public class ProductWarehouseRepository : IProductWarehouseRepository
{
    private readonly string _connectionString;

    public ProductWarehouseRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new ArgumentException("No connection string found");
    }

    public async Task<int?> GetProductWarehouseIdOfCompletedOrderAsync(int orderId)
    {
        const string sql = """
                           SELECT TOP 1 IdProductWarehouse
                           FROM Product_Warehouse
                           WHERE IdOrder = @IdOrder
                           """;

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();

        command.Parameters.AddWithValue("@IdOrder", orderId);
        var result = await command.ExecuteScalarAsync();

        return result == null || result == DBNull.Value ? 
            null : 
            Convert.ToInt32(result);
    }

    public async Task<int> SaveProductWarehouseAsync(ProductWarehouse productWarehouse)
    {
        const string updateOrderSql = """
                                      UPDATE [Order]
                                      SET FulfilledAt = @FulfilledAt
                                      WHERE IdOrder = @IdOrder
                                      """;

        const string insertWarehouseSql = """
                                          INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
                                          OUTPUT INSERTED.IdProductWarehouse
                                          VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)
                                          """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            await using (var updateCmd = new SqlCommand(updateOrderSql, connection, (SqlTransaction)transaction))
            {
                updateCmd.Parameters.AddWithValue("@FulfilledAt", productWarehouse.CreatedAt);
                updateCmd.Parameters.AddWithValue("@IdOrder", productWarehouse.Order.Id);
                await updateCmd.ExecuteNonQueryAsync();
            }
            
            await using (var insertCmd = new SqlCommand(insertWarehouseSql, connection, (SqlTransaction)transaction))
            {
                insertCmd.Parameters.AddWithValue("@IdWarehouse", productWarehouse.Warehouse.Id);
                insertCmd.Parameters.AddWithValue("@IdProduct", productWarehouse.Product.Id);
                insertCmd.Parameters.AddWithValue("@IdOrder", productWarehouse.Order.Id);
                insertCmd.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
                insertCmd.Parameters.AddWithValue("@Price", productWarehouse.Price);
                insertCmd.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);

                var insertedId = await insertCmd.ExecuteScalarAsync();
                await transaction.CommitAsync();

                return Convert.ToInt32(insertedId);
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<int> SaveProductWarehouseUsingStoredProcedureAsync(int productId, int warehouseId, int amount, DateTime createdAt)
    {
        const string procName = "AddProductToWarehouse";

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(procName, connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@IdProduct", productId);
        command.Parameters.AddWithValue("@IdWarehouse", warehouseId);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@CreatedAt", createdAt);

        await connection.OpenAsync();

        try
        {
            var result = await command.ExecuteScalarAsync();
            if (result == null || result == DBNull.Value)
            {
                throw new Exception("Stored procedure execution failed to return a new ID.");
            }

            return Convert.ToInt32(result);
        }
        catch (SqlException ex)
        {
            throw new Exception($"SQL error executing stored procedure: {ex.Message}", ex);
        }
    }
}