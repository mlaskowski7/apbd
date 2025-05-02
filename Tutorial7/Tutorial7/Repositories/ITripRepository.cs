using Tutorial7.Models;
using Tutorial7.Utils;

namespace Tutorial7.Repositories;

public interface ITripRepository
{
    public Task<ResultWrapper<IEnumerable<Trip>>> GetAllAsync();
}