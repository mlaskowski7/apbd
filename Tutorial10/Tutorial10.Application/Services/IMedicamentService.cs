using Tutorial10.Application.Repositories;
using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Services;

public interface IMedicamentService
{
    Task<Result<Medicament>> GetMedicamentByIdAsync(int medicamentId, CancellationToken cancellationToken = default);
}