using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Utils;

namespace VetClinicShelterApi.Services;

public interface IAnimalService
{
    ICollection<AnimalResponseDto> GetAllAnimals();
    
    ResultWrapper<AnimalResponseDto> GetAnimalById(int id);
    
    AnimalResponseDto CreateAnimal(AnimalRequestDto animal);
    
    ResultWrapper<AnimalResponseDto> UpdateAnimal(int id, AnimalRequestDto animal);
    
    bool DeleteAnimalById(int id);
}