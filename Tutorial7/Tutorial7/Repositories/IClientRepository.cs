using Tutorial7.Contracts.Request;
using Tutorial7.Contracts.Response;
using Tutorial7.Models;
using Tutorial7.Utils;

namespace Tutorial7.Repositories;

public interface IClientRepository
{
    Task<ResultWrapper<IEnumerable<Trip>>> GetClientTripsAsync(int id);
    
    Task<ResultWrapper<int>> CreateAsync(string firstName, string lastName, string email, string telephone, string pesel);
}