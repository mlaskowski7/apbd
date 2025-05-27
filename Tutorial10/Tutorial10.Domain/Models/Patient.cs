namespace Tutorial10.Domain.Models;

public class Patient
{
   public int IdPatient { get; set; }
   
   public required string FirstName { get; set; }
   
   public required string LastName { get; set; }
   
   public required DateTime BirthDate { get; set; }
   
   public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}