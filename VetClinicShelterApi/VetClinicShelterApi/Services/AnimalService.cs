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

        private readonly IVisitMapper _visitMapper;

        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalMapper animalMapper, IVisitMapper visitMapper, IAnimalRepository animalRepository)
        {
            _animalMapper = animalMapper;
            _visitMapper = visitMapper;
            _animalRepository = animalRepository;
        }

        public ResultWrapper<AnimalResponseDto> CreateAnimal(AnimalRequestDto animal)
        {
            var animalMapResult = _animalMapper.MapToModel(animal);
            if (!animalMapResult.IsOk)  
            {
                return ResultWrapper<AnimalResponseDto>.FromErr(animalMapResult);
            }
            var animalModel = animalMapResult.Result!;
            _animalRepository.SaveAnimal(animalModel);
            return ResultWrapper<AnimalResponseDto>.Ok(_animalMapper.MapToContract(animalModel));
        }

        public ResultWrapper<VisitResponseDto> CreateVisitForAnimal(VisitRequestDto visit)
        {
            var visitMapResult = _visitMapper.MapToModel(visit);
            if (!visitMapResult.IsOk)
            {
                return ResultWrapper<VisitResponseDto>.FromErr(visitMapResult);
            }

            var visitModel = visitMapResult.Result!;
            visitModel.Animal.Visits.Add(visitModel);
            return ResultWrapper<VisitResponseDto>.Ok(_visitMapper.MapToContract(visitModel));
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

        public ResultWrapper<ICollection<VisitResponseDto>> GetAllVisitsByAnimalId(Guid animalId)
        {
            var animal = _animalRepository.FindAnimalById(animalId);
            if (animal == null)
            {
                return ResultWrapper<ICollection<VisitResponseDto>>.Err($"Animal with id = {animalId} was not found");
            }

            var visitsResponse = animal.Visits
                                       .Select(v => _visitMapper.MapToContract(v))
                                       .ToList();
            return ResultWrapper<ICollection<VisitResponseDto>>.Ok(visitsResponse);
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

            var animalMapResult = _animalMapper.MapToModel(animal);
            if (!animalMapResult.IsOk)
            {
                return ResultWrapper<AnimalResponseDto>.FromErr(animalMapResult);
            }
            var animalModel = animalMapResult.Result!;
            animalModel.Id = id;
            _animalRepository.SaveAnimal(animalModel);
            var response = _animalMapper.MapToContract(animalModel);
            return ResultWrapper<AnimalResponseDto>.Ok(response);
        }
    }
}
