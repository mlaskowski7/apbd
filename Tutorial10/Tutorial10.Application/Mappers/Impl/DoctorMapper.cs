using Tutorial10.Application.Contracts.Response;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Mappers.Impl;

public class DoctorMapper : IDoctorMapper
{
    public DoctorResponseDto MapEntityToResponseDto(Doctor doctor)
    {
        return new DoctorResponseDto(doctor.IdDoctor, doctor.FirstName);
    }
}