using System.ComponentModel.DataAnnotations;

namespace Tutorial10.Application.Contracts.Request;

public class PrescriptionMedicamentRequestDto
{
    [Required]
    public int IdMedicament { get; set; }
    
    [Required]
    public int Dose { get; set; }
    
    [Required]
    public string Description { get; set; }
}