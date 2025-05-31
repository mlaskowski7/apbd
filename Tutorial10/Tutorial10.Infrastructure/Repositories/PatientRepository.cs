using Microsoft.EntityFrameworkCore;
using Tutorial10.Application.Repositories;
using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;
using Tutorial10.Infrastructure.Database;
using Tutorial10.Infrastructure.Utils;

namespace Tutorial10.Infrastructure.Repositories;

public class PatientRepository(ClinicDbContext clinicDbContext) : IPatientRepository
{
    public async Task<(Patient?, Error?)> FindPatientByIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbOperationsUtils.TryAsync<Patient>(async () =>
            await clinicDbContext.Patients.Include(p => p.Prescriptions)
                                          .ThenInclude(pr => pr.PrescriptionMedicaments)
                                          .ThenInclude(pm => pm.Medicament)
                                          .Include(p => p.Prescriptions)
                                          .ThenInclude(p => p.Doctor)
                                          .FirstOrDefaultAsync(d => d.IdPatient == patientId, cancellationToken));
    }

    public async Task<(Patient?, Error?)> CreatePatientAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        return await DbOperationsUtils.TryAsync<Patient>(async () =>
        {
            var createdPatient = await clinicDbContext.Patients.AddAsync(patient, cancellationToken);
            await clinicDbContext.SaveChangesAsync(cancellationToken);
            return createdPatient.Entity;
        });
    }
}