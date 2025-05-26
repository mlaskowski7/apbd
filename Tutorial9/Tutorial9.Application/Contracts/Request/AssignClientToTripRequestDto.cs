using System.ComponentModel.DataAnnotations;

namespace Tutorial9.Application.Contracts.Request;

public class AssignClientToTripRequestDto
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    
    [Required]
    public string Pesel { get; set; }
    
    public DateTime? PaymentDate { get; set; }
}