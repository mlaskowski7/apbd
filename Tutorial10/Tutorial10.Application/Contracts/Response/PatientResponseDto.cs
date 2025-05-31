using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Contracts.Response;

public record PatientResponseDto(
    int IdPatient,
    string FirstName,
    string LastName,
    DateTime BirthDate,
    List<PrescriptionResponseDto> Prescriptions);