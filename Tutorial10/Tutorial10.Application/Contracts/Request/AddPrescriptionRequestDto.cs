using System.ComponentModel.DataAnnotations;

namespace Tutorial10.Application.Contracts.Request;

public class AddPrescriptionRequestDto
{
    [Required]
    public GetOrCreatePatientRequestDto Patient { get; set; }
   
    [Required]
    [MinLength(1)]
    public List<PrescriptionMedicamentRequestDto> Medicaments { get; set; }
    
    [Required]
    public int DoctorId { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
   
    [Required]
    public DateTime DueDate { get; set; }
}