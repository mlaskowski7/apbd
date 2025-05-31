using Microsoft.EntityFrameworkCore;
using Tutorial10.Application.Repositories;
using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;
using Tutorial10.Infrastructure.Database;
using Tutorial10.Infrastructure.Utils;

namespace Tutorial10.Infrastructure.Repositories;

public class MedicamentRepository(ClinicDbContext clinicDbContext) : IMedicamentRepository
{
    public async Task<(Medicament?, Error?)> FindMedicamentByIdAsync(int medicamentId, CancellationToken cancellationToken = default)
    {
        return await DbOperationsUtils.TryAsync<Medicament>(async () =>
        {
            return await clinicDbContext.Medicaments.FirstOrDefaultAsync(d => d.IdMedicament == medicamentId,
                cancellationToken);
        });
    }
}