using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Repositories;

public interface IDoctorRepository
{
    Task<(Doctor?, Error?)> FindDoctorById(int doctorId, CancellationToken cancellationToken = default);
    
}