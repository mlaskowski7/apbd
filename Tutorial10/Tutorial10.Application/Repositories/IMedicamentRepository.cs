using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Repositories;

public interface IMedicamentRepository
{
    Task<(Medicament?, Error?)> FindMedicamentByIdAsync(int medicamentId, CancellationToken cancellationToken = default);
}