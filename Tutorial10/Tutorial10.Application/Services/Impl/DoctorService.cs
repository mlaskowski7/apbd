using Tutorial10.Application.Repositories;
using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Services.Impl;

public class DoctorService(IDoctorRepository doctorRepository) : IDoctorService
{
    public async Task<Result<Doctor>> GetDoctorById(int doctorId, CancellationToken cancellationToken = default)
    {
       var (doctor, err) = await doctorRepository.FindDoctorById(doctorId, cancellationToken);
       if (err != null)
       {
           return Result<Doctor>.Err(err);
       }

       if (doctor == null)
       {
           return Result<Doctor>.Err(Error.NotFound($"Doctor with id = {doctorId} was not found"));
       }

       return Result<Doctor>.Ok(doctor);
    }
}