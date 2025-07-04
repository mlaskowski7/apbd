using System.ComponentModel.DataAnnotations;

namespace Tutorial10.Application.Contracts.Request;

public class GetOrCreatePatientRequestDto
{
   public int? IdPatient { get; set; }
   
   public string? FirstName { get; set; }
   
   public string? LastName { get; set; }
   
   public DateTime? BirthDate { get; set; }
}