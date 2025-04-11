using System.ComponentModel.DataAnnotations;
using VetClinicShelterApi.Models;

namespace VetClinicShelterApi.Dtos.Request;

public class AnimalRequestDto()
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
    public double Weight { get; set; }
    
    [Required]
    public AnimalCategory Category { get; set; }
    
    [Required]
    [RegularExpression(
        "^#([A-Fa-f0-9]{6})$", 
        ErrorMessage = "FurColor must be in a form of a valid hex color code.")]
    public string FurColor { get; set; }
}