using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Models;

namespace VetClinicShelterApi.Mappers
{
    public interface IVisitMapper 
        : IContractMapper<VisitRequestDto, VisitResponseDto, Visit>
    {
    }
}
