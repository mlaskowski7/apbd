using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Utils;

namespace VetClinicShelterApi.Services;

public interface IAnimalService
{
    ICollection<AnimalResponseDto> GetAllAnimals();
    
    ResultWrapper<AnimalResponseDto> GetAnimalById(Guid id);
    
    ResultWrapper<AnimalResponseDto> CreateAnimal(AnimalRequestDto animal);
    
    ResultWrapper<AnimalResponseDto> UpdateAnimal(Guid id, AnimalRequestDto animal);
    
    bool DeleteAnimalById(Guid id);

    ResultWrapper<VisitResponseDto> CreateVisitForAnimal(VisitRequestDto visit);

    ResultWrapper<ICollection<VisitResponseDto>> GetAllVisitsByAnimalId(Guid animalId);
}