using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Mappers;
using VetClinicShelterApi.Repositories;
using VetClinicShelterApi.Utils;

namespace VetClinicShelterApi.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalMapper _animalMapper;

        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalMapper animalMapper, IAnimalRepository animalRepository)
        {
            _animalMapper = animalMapper;
            _animalRepository = animalRepository;
        }

        public AnimalResponseDto CreateAnimal(AnimalRequestDto animal)
        {
            var animalModel = _animalMapper.MapToModel(animal);
            _animalRepository.SaveAnimal(animalModel);
            return _animalMapper.MapToContract(animalModel);
        }

        public bool DeleteAnimalById(Guid id)
        {
            return _animalRepository.DeleteAnimalById(id);
        }

        public ICollection<AnimalResponseDto> GetAllAnimals()
        {
            var animals = _animalRepository.FindAllAnimals();
            return animals.Select(animal => _animalMapper.MapToContract(animal))
                          .ToList();
        }

        public ResultWrapper<AnimalResponseDto> GetAnimalById(Guid id)
        {
            var animal = _animalRepository.FindAnimalById(id);
            if (animal == null)
            {
                return ResultWrapper<AnimalResponseDto>.Err($"Animal with id = {id} was not found");
            }
            var response = _animalMapper.MapToContract(animal);
            return ResultWrapper<AnimalResponseDto>.Ok(response);
        }

        public ResultWrapper<AnimalResponseDto> UpdateAnimal(Guid id, AnimalRequestDto animal)
        {
            var isDeleted = _animalRepository.DeleteAnimalById(id);
            if (!isDeleted)
            {
                return ResultWrapper<AnimalResponseDto>.Err($"Animal with id = {id} cant be updated as it was not found");
            }

            var animalModel = _animalMapper.MapToModel(animal);
            animalModel.Id = id;
            _animalRepository.SaveAnimal(animalModel);
            var response = _animalMapper.MapToContract(animalModel);
            return ResultWrapper<AnimalResponseDto>.Ok(response);
        }
    }
}
