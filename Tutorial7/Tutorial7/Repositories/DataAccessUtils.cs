using System.Net;
using Microsoft.Data.SqlClient;
using Tutorial7.Utils;

namespace Tutorial7.Repositories;

public static class DataAccessUtils
{
    public static async Task<ResultWrapper<T>> TryExecuteAsync<T>(Func<Task<T>> func)
    {
        try
        {
            var result = await func();
            return ResultWrapper<T>.Ok(result);
        }
        catch (SqlException ex)
        {
            return ResultWrapper<T>.Err("Database error: " + ex.Message, (int)HttpStatusCode.InternalServerError);
        }
        catch (InvalidCastException ex)
        {
            return ResultWrapper<T>.Err("Data conversion error: " + ex.Message, (int)HttpStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            return ResultWrapper<T>.Err("Unexpected error: " + ex.Message, (int)HttpStatusCode.InternalServerError);
        }
    }
}