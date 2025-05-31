using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Repositories;

public interface IPrescriptionRepository
{
   Task<(Prescription?, Error?)> CreatePrescriptionAsync(Prescription prescription);
}