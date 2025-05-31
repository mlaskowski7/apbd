using Tutorial10.Application.Contracts.Request;
using Tutorial10.Application.Contracts.Response;
using Tutorial10.Application.Utils;

namespace Tutorial10.Application.Services.Impl;

public class PrescriptionService : IPrescriptionService
{
    public async Task<Result<PrescriptionResponseDto>> AddPrescriptionAsync(AddPrescriptionRequestDto addPrescriptionRequestDto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}