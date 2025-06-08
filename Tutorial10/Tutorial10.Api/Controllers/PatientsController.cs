using Microsoft.AspNetCore.Mvc;
using Tutorial10.Api.Extensions;
using Tutorial10.Application.Services;

namespace Tutorial10.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IPatientService patientService) : ControllerBase
{
    [HttpGet("{patientId:int}")]
    public async Task<IActionResult> GetPatientByIdAsync(
        [FromRoute] int patientId, 
        CancellationToken cancellationToken = default)
    {
        var patientResult = await patientService.GetPatientByIdAsync(patientId, cancellationToken);

        if (patientResult.IsOk)
        {
            var patient = patientResult.Value;
            return Ok(patient);
        }
        
        return this.ResponseFromErrResult(patientResult);
    }
}