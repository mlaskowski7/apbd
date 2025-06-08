using Tutorial10.Application.Contracts.Request;
using Tutorial10.Application.Contracts.Response;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Mappers;

public interface IPrescriptionMapper
{
    Prescription MapAddPrescriptionDtoToEntity(AddPrescriptionRequestDto prescription);
    
    PrescriptionResponseDto MapEntityToResponseDto(Prescription prescription);
}