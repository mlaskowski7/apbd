using Tutorial10.Application.Contracts.Request;
using Tutorial10.Application.Repositories;
using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Services.Impl;

public class PatientService(IPatientRepository patientRepository) : IPatientService
{
    public async Task<Result<Patient>> GetOrCreatePatientAsync(GetOrCreatePatientRequestDto getOrCreatePatientRequestDto, CancellationToken cancellationToken = default)
    {
        if (getOrCreatePatientRequestDto.IdPatient != null)
        {
           return await GetPatientByIdAsync(getOrCreatePatientRequestDto.IdPatient.Value, cancellationToken); 
        }

        if (string.IsNullOrEmpty(getOrCreatePatientRequestDto.FirstName) ||
            string.IsNullOrEmpty(getOrCreatePatientRequestDto.LastName) ||
            getOrCreatePatientRequestDto.BirthDate == null)
        {
            return Result<Patient>.Err(
                Error.BadRequest(
                    "One of the patient's field was missing. Provide either patientID or all his details."));
        }
        
        return await CreatePatient(getOrCreatePatientRequestDto.FirstName, getOrCreatePatientRequestDto.LastName, getOrCreatePatientRequestDto.BirthDate.Value, cancellationToken);
            
    }

    private async Task<Result<Patient>> GetPatientByIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        var (foundPatient, err) = await patientRepository.FindPatientByIdAsync(patientId, cancellationToken);
        if (err != null)
        {
            return Result<Patient>.Err(err);
        }

        if (foundPatient == null)
        {
            return Result<Patient>.Err(Error.NotFound($"Patient with id = ${patientId} was not found"));
        }
        
        return Result<Patient>.Ok(foundPatient);
    }

    private async Task<Result<Patient>> CreatePatient(string firstName, string lastName, DateTime birthDate,
        CancellationToken cancellationToken = default)
    {
        var patient = new Patient
        {
            FirstName = firstName,
            LastName = lastName,
            BirthDate = birthDate,
        };
        
        var (createdPatient, err) = await patientRepository.CreatePatientAsync(patient, cancellationToken);
        if (err != null)
        {
            return Result<Patient>.Err(err);
        }
        
        return Result<Patient>.Ok(createdPatient!);
    }
}