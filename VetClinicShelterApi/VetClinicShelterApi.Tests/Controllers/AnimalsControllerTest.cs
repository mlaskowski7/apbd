using Microsoft.AspNetCore.Mvc;
using Moq;
using VetClinicShelterApi.Controllers;
using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Models;
using VetClinicShelterApi.Services;
using VetClinicShelterApi.Utils;

namespace VetClinicShelterApi.Tests.Controllers;

public class AnimalsControllerTest
{
    
    private readonly Mock<IAnimalService> _animalServiceMock;
    
    private readonly AnimalsController _animalsController;

    public AnimalsControllerTest()
    {
        _animalServiceMock = new Mock<IAnimalService>();
        _animalsController = new AnimalsController(_animalServiceMock.Object);
    }

    [Fact]
    public void GetAllAnimals_WhenSuccessfulServiceCall_ShouldReturnAllAnimals()
    {
        // arrange
        _animalServiceMock.Setup(x => x.GetAllAnimals())
                          .Returns(new List<AnimalResponseDto>()
                          {
                              GetSampleAnimalResponseDto()
                          });
        
        // act
        var result = _animalsController.GetAllAnimals();
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAnimals = Assert.IsAssignableFrom<ICollection<AnimalResponseDto>>(okObjectResult.Value);
            Assert.Single(returnedAnimals);
        });
    }
    
    [Fact]
    public void GetAnimalById_WhenFound_ShouldReturnOkWithAnimal()
    {
        // arrange
        var expected = GetSampleAnimalResponseDto();
        _animalServiceMock.Setup(s => s.GetAnimalById(expected.Id))
                          .Returns(ResultWrapper<AnimalResponseDto>.Ok(expected));

        // act
        var result = _animalsController.GetAnimalById(expected.Id);

        // assert
        Assert.Multiple(() =>
        {
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var animal = Assert.IsType<AnimalResponseDto>(okObjectResult.Value);
            Assert.Equal("test", animal.Name);
        });
    }

    [Fact]
    public void GetAnimalById_WhenNotFound_ReturnsNotFound()
    {
        // arrange
        _animalServiceMock.Setup(s => s.GetAnimalById(Guid.Empty))
                          .Returns(ResultWrapper<AnimalResponseDto>.Err("test"));

        // act
        var result = _animalsController.GetAnimalById(Guid.Empty);

        // assert
        Assert.Multiple(() =>
        {
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("test", notFoundObjectResult.Value);
        });
    }
    
    [Fact]
    public void CreateAnimal_WhenSuccessfulServiceCall_ShouldReturnOkWithCreatedAnimal()
    {
        // arrange
        var request = GetSampleAnimalRequestDto();
        var response = GetSampleAnimalResponseDto();
        
        _animalServiceMock.Setup(s => s.CreateAnimal(request))
                          .Returns(response);
        
        // act
        var result = _animalsController.CreateAnimal(request);

        // assert
        Assert.Multiple(() =>
        {
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var animalResponse = Assert.IsType<AnimalResponseDto>(createdAtActionResult.Value);
            Assert.Equal("test", animalResponse.Name);
        });
    }

    [Fact]
    public void UpdateAnimal_WhenFound_ShouldReturnOkWithUpdatedAnimal()
    {
        // arrange
        var request = GetSampleAnimalRequestDto();
        var response = GetSampleAnimalResponseDto();

        _animalServiceMock.Setup(s => s.UpdateAnimal(response.Id, request))
                          .Returns(ResultWrapper<AnimalResponseDto>.Ok(response));

        // act
        var result = _animalsController.UpdateAnimal(response.Id, request);

        // arrange
        Assert.Multiple(() =>
        {
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var animalResponse = Assert.IsType<AnimalResponseDto>(okObjectResult.Value);
            Assert.Equal("test", animalResponse.Name);
        });
    }

    [Fact]
    public void UpdateAnimal_WhenNotFound_ShouldReturnNotFound()
    {
        // arrange
        var request = new AnimalRequestDto { Name = "not found" };
        var missingId = Guid.NewGuid();

        _animalServiceMock.Setup(s => s.UpdateAnimal(missingId, request))
                          .Returns(ResultWrapper<AnimalResponseDto>.Err("Not found"));

        // act
        var result = _animalsController.UpdateAnimal(missingId, request);

        // assert
        Assert.Multiple(() =>
        {
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Not found", notFound.Value);
        });
    }

    [Fact]
    public void DeleteAnimalById_WhenFound_ShouldReturnNoContent()
    {
        // arrange
        var existingId = Guid.NewGuid();
        _animalServiceMock.Setup(s => s.DeleteAnimalById(existingId))
                          .Returns(true);

        // act
        var result = _animalsController.DeleteAnimalById(existingId);

        // assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteAnimalById_WhenNotFound_ShouldReturnNotFound()
    {
        // arrange
        var missingId = Guid.NewGuid();
        _animalServiceMock.Setup(s => s.DeleteAnimalById(missingId))
                          .Returns(false);

        // act
        var result = _animalsController.DeleteAnimalById(missingId);

        // assert
        Assert.IsType<NotFoundResult>(result);
    }


    private AnimalRequestDto GetSampleAnimalRequestDto()
    {
        return new()
        {
            Name = "test",
            Weight = 10.0,
            Category = AnimalCategory.Dog,
            FurColor = "#FFFFFF",
        };
    }

    private AnimalResponseDto GetSampleAnimalResponseDto()
    {
        return new AnimalResponseDto(Guid.NewGuid(), "test", 10.0, AnimalCategory.Dog, "#FFFFFF");
    }
}