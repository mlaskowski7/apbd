namespace Tutorial10.Domain.Models;

public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    
    public int IdPrescription { get; set; }
    
    public required int Dose { get; set; }
    
    public required string Details { get; set; }
    
    public virtual Medicament Medicament { get; set; }
    
    public virtual Prescription Prescription { get; set; }
}