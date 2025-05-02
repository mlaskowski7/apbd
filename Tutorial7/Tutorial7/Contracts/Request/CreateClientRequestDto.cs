using System.ComponentModel.DataAnnotations;

namespace Tutorial7.Contracts.Request;

public class CreateClientRequestDto
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
    public string Telephone { get; set; }
    
    [Required]
    public string Pesel { get; set; }
}