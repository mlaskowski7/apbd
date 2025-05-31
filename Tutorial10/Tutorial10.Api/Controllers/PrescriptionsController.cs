using Microsoft.AspNetCore.Mvc;
using Tutorial10.Api.Extensions;
using Tutorial10.Application.Contracts.Request;
using Tutorial10.Application.Services;

namespace Tutorial10.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PrescriptionsController(IPrescriptionService prescriptionService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionRequestDto addPrescriptionRequestDto)
    {
        var prescriptionResult = await prescriptionService.AddPrescriptionAsync(addPrescriptionRequestDto);

        if (prescriptionResult.IsOk)
        {
            var prescription = prescriptionResult.Value;
            return Created($"/api/prescriptions/{prescription.IdPrescription}", prescription);
        }
        
        return this.ResponseFromErrResult(prescriptionResult);
    }
}