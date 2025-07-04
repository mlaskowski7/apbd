namespace Tutorial10.Domain.Models;

public class Prescription
{
    public int IdPrescription { get; set; }
    
    public required DateTime Date { get; set; }
    
    public required DateTime DueDate { get; set; }
    
    public int IdPatient { get; set; }
    
    public int IdDoctor { get; set; }
    
    public virtual Patient Patient { get; set; }
    
    public virtual Doctor Doctor { get; set; }

    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}