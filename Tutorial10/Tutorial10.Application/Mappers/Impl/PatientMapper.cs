using Tutorial10.Application.Contracts.Response;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Mappers.Impl;

public class PatientMapper(IPrescriptionMapper prescriptionMapper) : IPatientMapper
{
    public PatientResponseDto MapEntityToResponseDto(Patient patient)
    {
        var prescriptions = patient.Prescriptions.Select(prescriptionMapper.MapEntityToResponseDto)
                                                 .OrderBy(p => p.DueDate)
                                                 .ToList();

        return new PatientResponseDto(
            patient.IdPatient,
            patient.FirstName,
            patient.LastName,
            patient.BirthDate,
            prescriptions);
    }
}