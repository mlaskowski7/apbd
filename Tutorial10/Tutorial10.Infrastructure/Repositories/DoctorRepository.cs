using Microsoft.EntityFrameworkCore;
using Tutorial10.Application.Repositories;
using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;
using Tutorial10.Infrastructure.Database;
using Tutorial10.Infrastructure.Utils;

namespace Tutorial10.Infrastructure.Repositories;

public class DoctorRepository(ClinicDbContext clinicDbContext) : IDoctorRepository
{
    public async Task<(Doctor?, Error?)> FindDoctorById(int doctorId, CancellationToken cancellationToken = default)
    {
        return await DbOperationsUtils.TryAsync<Doctor>(async () =>
            await clinicDbContext.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == doctorId, cancellationToken));
    }
}