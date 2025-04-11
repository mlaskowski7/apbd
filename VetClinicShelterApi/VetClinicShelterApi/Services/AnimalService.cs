using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Mappers;
using VetClinicShelterApi.Utils;

namespace VetClinicShelterApi.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalMapper _animalMapper;

        public AnimalService(IAnimalMapper animalMapper)
        {
            _animalMapper = animalMapper;
        }

        public AnimalResponseDto CreateAnimal(AnimalRequestDto animal)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAnimalById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<AnimalResponseDto> GetAllAnimals()
        {
            throw new NotImplementedException();
        }

        public ResultWrapper<AnimalResponseDto> GetAnimalById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ResultWrapper<AnimalResponseDto> UpdateAnimal(Guid id, AnimalRequestDto animal)
        {
            throw new NotImplementedException();
        }
    }
}
