using System.ComponentModel.DataAnnotations;
using VetClinicShelterApi.Models;

namespace VetClinicShelterApi.Dtos.Request
{
    public class VisitRequestDto
    {
        [Required]
        public DateTime DateOfVisit { get; set; }

        [Required]
        public Guid AnimalId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }
    }
}
