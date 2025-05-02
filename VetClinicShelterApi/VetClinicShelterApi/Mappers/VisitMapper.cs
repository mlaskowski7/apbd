using System.Reflection;
using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Models;
using VetClinicShelterApi.Repositories;
using VetClinicShelterApi.Utils;

namespace VetClinicShelterApi.Mappers
{
    public class VisitMapper : IVisitMapper
    {
        private readonly IAnimalRepository _animalRepository;

        private readonly IAnimalMapper _animalMapper;

        public VisitMapper(IAnimalRepository animalRepository, IAnimalMapper animalMapper)
        {
            _animalRepository = animalRepository;
            _animalMapper = animalMapper;
        }

        public VisitResponseDto MapToContract(Visit model)
        {
            var animalResponse = _animalMapper.MapToContract(model.Animal);
            return new VisitResponseDto(model.DateOfVisit, animalResponse, model.Description, model.Price);
        }

        public ResultWrapper<Visit> MapToModel(VisitRequestDto requestDto)
        {
            var animal = _animalRepository.FindAnimalById(requestDto.AnimalId);
            if (animal == null)
            {
                return ResultWrapper<Visit>.Err($"Animal with provided id ({requestDto.AnimalId}) was not found");
            }

            var visit = new Visit()
            {
                DateOfVisit = requestDto.DateOfVisit,
                Animal = animal,
                Description = requestDto.Description,
                Price = requestDto.Price,
            };

            return ResultWrapper<Visit>.Ok(visit);
        }
    }
}
