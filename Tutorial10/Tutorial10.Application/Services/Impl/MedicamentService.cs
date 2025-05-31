using Tutorial10.Application.Repositories;
using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Services.Impl;

public class MedicamentService(IMedicamentRepository medicamentRepository) : IMedicamentService
{
    public async Task<Result<Medicament>> GetMedicamentByIdAsync(int medicamentId, CancellationToken cancellationToken = default)
    {
        var (medicament, err) = await medicamentRepository.FindMedicamentByIdAsync(medicamentId, cancellationToken);
        if (err != null)
        {
            return Result<Medicament>.Err(err);
        }

        if (medicament == null)
        {
            return Result<Medicament>.Err(Error.NotFound($"Medicament with id = {medicamentId} was not found"));
        }
        
        return Result<Medicament>.Ok(medicament);
    }
}