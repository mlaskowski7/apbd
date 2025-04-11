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

    [HttpGet("{id:int}")]
    public ActionResult<AnimalResponseDto> GetAnimalById([FromRoute] int id)
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
        return Ok(createdAnimal);
    }

    [HttpPut("{id:int}")]
    public ActionResult<AnimalResponseDto> UpdateAnimal([FromRoute] int id, [FromBody] AnimalRequestDto animalBody)
    {
        var updatedAnimalResult = animalService.UpdateAnimal(id, animalBody);
        if (updatedAnimalResult.IsOk)
        {
            return Ok(updatedAnimalResult.Result);
        }
        
        return NotFound(updatedAnimalResult.ErrorMessage);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimalById(int id)
    {
        var isDeleted = animalService.DeleteAnimalById(id);
        return isDeleted ? NoContent() : NotFound();
    }
}