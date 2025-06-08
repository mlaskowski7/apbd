using Tutorial10.Application.Repositories;
using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;
using Tutorial10.Infrastructure.Database;
using Tutorial10.Infrastructure.Utils;

namespace Tutorial10.Infrastructure.Repositories;

public class PrescriptionRepository(ClinicDbContext clinicDbContext) : IPrescriptionRepository
{
    public async Task<(Prescription?, Error?)> CreatePrescriptionAsync(Prescription prescription, CancellationToken cancellationToken = default)
    {
        return await DbOperationsUtils.TryAsync<Prescription>(async () =>
        {
            var createdPrescription = await clinicDbContext.Prescriptions.AddAsync(prescription, cancellationToken);
            await clinicDbContext.SaveChangesAsync(cancellationToken);
            return createdPrescription.Entity;
        });
    }
}