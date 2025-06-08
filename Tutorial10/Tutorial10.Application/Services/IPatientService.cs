using Tutorial10.Application.Contracts.Request;
using Tutorial10.Application.Contracts.Response;
using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Services;

public interface IPatientService
{
   Task<Result<Patient>> GetOrCreatePatientAsync(GetOrCreatePatientRequestDto getOrCreatePatientRequestDto, CancellationToken cancellationToken = default); 
   
   Task<Result<PatientResponseDto>> GetPatientByIdAsync(int patientId, CancellationToken cancellationToken = default);
}