namespace Tutorial10.Domain.Models;

public class Medicament
{
    public int IdMedicament { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public required string Type { get; set; }
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}