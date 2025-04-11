using System.Drawing;
using VetClinicShelterApi.Models;

namespace VetClinicShelterApi.Dtos.Response;

public record AnimalResponseDto(Guid Id, string Name, decimal Weight, AnimalCategory Category, string Color);