using Tutorial10.Application.Contracts.Response;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Mappers;

public interface IDoctorMapper
{
    DoctorResponseDto MapEntityToResponseDto(Doctor doctor);
}