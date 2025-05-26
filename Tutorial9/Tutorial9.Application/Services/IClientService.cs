using Tutorial9.Application.Utils;

namespace Tutorial9.Application.Services;

public interface IClientService
{
    Task<Error?> DeleteClientByIdAsync(int id, CancellationToken cancellationToken = default);
}