namespace Tutorial10.Application.Contracts.Response;

public record PrescriptionMedicamentResponseDto(
    int IdMedicament,
    string Name,
    int Dose,
    string Description);