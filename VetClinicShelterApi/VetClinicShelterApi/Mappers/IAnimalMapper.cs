using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Models;

namespace VetClinicShelterApi.Mappers
{
    public interface IAnimalMapper 
        : IContractMapper<AnimalRequestDto, AnimalResponseDto, Animal>
    {
    }
}
