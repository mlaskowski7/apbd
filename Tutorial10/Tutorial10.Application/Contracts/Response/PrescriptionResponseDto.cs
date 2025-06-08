namespace Tutorial10.Application.Contracts.Response;

public record PrescriptionResponseDto(
    int IdPrescription,
    DateTime Date,
    DateTime DueDate,
    List<PrescriptionMedicamentResponseDto> Medicaments,
    DoctorResponseDto Doctor);