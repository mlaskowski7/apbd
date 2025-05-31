namespace Tutorial10.Application.Contracts.Response;

public record MedicamentResponseDto(
    int IdMedicament,
    string Name,
    int Dose,
    string Description);