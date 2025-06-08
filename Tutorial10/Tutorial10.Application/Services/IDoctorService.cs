using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Services;

public interface IDoctorService
{
    Task<Result<Doctor>> GetDoctorById(int doctorId, CancellationToken cancellationToken = default);
}