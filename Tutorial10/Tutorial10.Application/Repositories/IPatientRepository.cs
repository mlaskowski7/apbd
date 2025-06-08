using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Repositories;

public interface IPatientRepository
{
    Task<(Patient?, Error?)> FindPatientByIdAsync(int patientId, CancellationToken cancellationToken = default);
    
    Task<(Patient?, Error?)> CreatePatientAsync(Patient patient, CancellationToken cancellationToken = default);
}