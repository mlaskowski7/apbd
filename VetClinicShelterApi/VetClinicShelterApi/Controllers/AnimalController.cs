using Microsoft.AspNetCore.Mvc;
using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Services;

namespace VetClinicShelterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController(IAnimalService _animalService) : ControllerBase
{
    [HttpGet]
    public ActionResult<ICollection<AnimalResponseDto>> GetAllAnimals()
    {
        return Ok(_animalService.GetAllAnimals());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<AnimalResponseDto> GetAnimalById([FromRoute] Guid id)
    {
        var animalResult = _animalService.GetAnimalById(id);
        if (animalResult.IsOk)
        {
            return Ok(animalResult.Result);
        }
        
        return NotFound(animalResult.ErrorMessage);
    }

    [HttpPost]
    public ActionResult<AnimalResponseDto> CreateAnimal([FromBody] AnimalRequestDto animalBody)
    {
        var createdAnimalResult = _animalService.CreateAnimal(animalBody);
        if (createdAnimalResult.IsOk)
        {
            return CreatedAtAction(nameof(GetAnimalById), new { id = createdAnimalResult.Result!.Id }, createdAnimalResult.Result);
        }
        
        return BadRequest(createdAnimalResult.ErrorMessage);
    }

    [HttpPut("{id:guid}")]
    public ActionResult<AnimalResponseDto> UpdateAnimal([FromRoute] Guid id, [FromBody] AnimalRequestDto animalBody)
    {
        var updatedAnimalResult = _animalService.UpdateAnimal(id, animalBody);
        if (updatedAnimalResult.IsOk)
        {
            return Ok(updatedAnimalResult.Result);
        }
        
        return NotFound(updatedAnimalResult.ErrorMessage);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteAnimalById([FromRoute] Guid id)
    {
        var isDeleted = _animalService.DeleteAnimalById(id);
        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPost("visits")]
    public ActionResult<VisitResponseDto> CreateVisit([FromBody] VisitRequestDto visit)
    {
        var createdVisitResult = _animalService.CreateVisitForAnimal(visit);
        if (createdVisitResult.IsOk)
        {
            return CreatedAtAction(nameof(GetAllVisitsByAnimalId), createdVisitResult.Result);
        }

        return BadRequest(createdVisitResult.ErrorMessage);
    }

    [HttpGet("{id:guid}/visits")]
    public ActionResult<ICollection<VisitResponseDto>> GetAllVisitsByAnimalId([FromRoute] Guid id)
    {
        var visitsResult = _animalService.GetAllVisitsByAnimalId(id);
        if (visitsResult.IsOk)
        {
            return Ok(visitsResult.Result);
        }

        return NotFound(visitsResult.ErrorMessage);
    }
}