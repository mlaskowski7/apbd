using Microsoft.AspNetCore.Mvc;
using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Services;

namespace VetClinicShelterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController(IAnimalService animalService) : ControllerBase
{
    [HttpGet]
    public ActionResult<ICollection<AnimalResponseDto>> GetAllAnimals()
    {
        return Ok(animalService.GetAllAnimals());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<AnimalResponseDto> GetAnimalById([FromRoute] Guid id)
    {
        var animalResult = animalService.GetAnimalById(id);
        if (animalResult.IsOk)
        {
            return Ok(animalResult.Result);
        }
        
        return NotFound(animalResult.ErrorMessage);
    }

    [HttpPost]
    public ActionResult<AnimalResponseDto> CreateAnimal([FromBody] AnimalRequestDto animalBody)
    {
        var createdAnimal = animalService.CreateAnimal(animalBody);
        return CreatedAtAction(nameof(GetAnimalById), createdAnimal);
    }

    [HttpPut("{id:guid}")]
    public ActionResult<AnimalResponseDto> UpdateAnimal([FromRoute] Guid id, [FromBody] AnimalRequestDto animalBody)
    {
        var updatedAnimalResult = animalService.UpdateAnimal(id, animalBody);
        if (updatedAnimalResult.IsOk)
        {
            return Ok(updatedAnimalResult.Result);
        }
        
        return NotFound(updatedAnimalResult.ErrorMessage);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteAnimalById(Guid id)
    {
        var isDeleted = animalService.DeleteAnimalById(id);
        return isDeleted ? NoContent() : NotFound();
    }
}