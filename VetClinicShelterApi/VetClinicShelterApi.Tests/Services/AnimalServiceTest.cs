using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Mappers;
using VetClinicShelterApi.Models;
using VetClinicShelterApi.Repositories;
using VetClinicShelterApi.Services;
using VetClinicShelterApi.Utils;

namespace VetClinicShelterApi.Tests.Services
{
    public class AnimalServiceTest
    {
        private readonly Mock<IAnimalMapper> _mapperMock;

        private readonly Mock<IAnimalRepository> _repositoryMock;

        private readonly AnimalService _animalService;

        public AnimalServiceTest()
        {
            _mapperMock = new Mock<IAnimalMapper>();
            _repositoryMock = new Mock<IAnimalRepository>();
            _animalService = new AnimalService(_mapperMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public void CreateAnimal_WhenHappyPath_ShouldMapSaveAnimalAndReturnDto()
        {
            // arrange
            var request = GetSampleAnimalRequest();
            var model = GetSampleAnimalModel();
            var response = GetSampleAnimalResponse();

            _mapperMock.Setup(m => m.MapToModel(request))
                       .Returns(ResultWrapper<Animal>.Ok(model));
            _mapperMock.Setup(m => m.MapToContract(model))
                       .Returns(response);

            // act
            var result = _animalService.CreateAnimal(request);
            var animal = result.Result!;

            // assert
            Assert.Multiple(() =>
            {
                Assert.Equal("test1", animal.Name);
                _repositoryMock.Verify(r => r.SaveAnimal(model), Times.Once);
            });
        }

        [Fact]
        public void DeleteAnimalById_WhenFound_ShouldReturnTrue_()
        {
            // arrange
            var id = Guid.NewGuid();
            _repositoryMock.Setup(r => r.DeleteAnimalById(id))
                           .Returns(true);

            // act
            var result = _animalService.DeleteAnimalById(id);

            // assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteAnimalById_WhenNotFound_ShouldReturnFalse()
        {
            // arrange
            var id = Guid.NewGuid();
            _repositoryMock.Setup(r => r.DeleteAnimalById(id)).Returns(false);

            // act
            var result = _animalService.DeleteAnimalById(id);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void GetAllAnimals_ShouldReturnMappedDtos()
        {
            // arrange
            var model = GetSampleAnimalModel();
            var dto = GetSampleAnimalResponse();

            _repositoryMock.Setup(r => r.FindAllAnimals())
                           .Returns(new List<Animal> { model });
            _mapperMock.Setup(m => m.MapToContract(model))
                       .Returns(dto);

            // act
            var result = _animalService.GetAllAnimals();

            // assert
            Assert.Multiple(() =>
            {
                Assert.Single(result);
                Assert.Equal("test1", result.First()
                                            .Name);
            });
        }

        [Fact]
        public void GetAnimalById_WhenFound_ShouldReturnOk()
        {
            // arrange
            var id = Guid.NewGuid();
            var model = GetSampleAnimalModel();
            var dto = GetSampleAnimalResponse();

            _repositoryMock.Setup(r => r.FindAnimalById(id))
                           .Returns(model);
            _mapperMock.Setup(m => m.MapToContract(model))
                       .Returns(dto);

            // act
            var result = _animalService.GetAnimalById(id);

            // assert
            Assert.Multiple(() =>
            {
                Assert.True(result.IsOk);
                Assert.Equal("test1", result.Result?.Name);
            });
        }

        [Fact]
        public void GetAnimalById_WhenNotFound_ShouldReturnErr()
        {
            // arrange
            var id = Guid.NewGuid();
            _repositoryMock.Setup(r => r.FindAnimalById(id))
                           .Returns((Animal?)null);

            // act
            var result = _animalService.GetAnimalById(id);

            // assert
            Assert.Multiple(() =>
            {
                Assert.False(result.IsOk);
                Assert.Null(result.Result);
                Assert.Contains("not found", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
            });
        }

        [Fact]
        public void UpdateAnimal_WhenExists_ShouldReturnUpdatedDto()
        {
            // arrange
            var id = Guid.NewGuid();
            var request = GetSampleAnimalRequest();
            var model = GetSampleAnimalModel();
            var dto = GetSampleAnimalResponse();

            _repositoryMock.Setup(r => r.DeleteAnimalById(id))
                           .Returns(true);
            _mapperMock.Setup(m => m.MapToModel(request))
                       .Returns(ResultWrapper<Animal>.Ok(model));
            _mapperMock.Setup(m => m.MapToContract(It.Is<Animal>(a => a.Id == id)))
                       .Returns(dto);

            // act
            var result = _animalService.UpdateAnimal(id, request);

            // assert
            Assert.Multiple(() =>
            {
                Assert.True(result.IsOk);
                Assert.Equal("test1", result.Result?.Name);
                Assert.Equal(id, model.Id);
                _repositoryMock.Verify(r => r.SaveAnimal(model), Times.Once);
            });
        }

        [Fact]
        public void UpdateAnimal_WhenNotExist_ShouldReturnErr()
        {
            // arrange
            var id = Guid.NewGuid();
            var request = GetSampleAnimalRequest();

            _repositoryMock.Setup(r => r.DeleteAnimalById(id))
                           .Returns(false);

            // act
            var result = _animalService.UpdateAnimal(id, request);

            // assert
            Assert.Multiple(() =>
            {
                Assert.False(result.IsOk);
                Assert.Null(result.Result);
                Assert.Contains("cant be updated", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
            });
        }

        private AnimalRequestDto GetSampleAnimalRequest() => new()
        {
            Name = "test1",
            Weight = 5.0,
            Category = AnimalCategory.Cat,
            FurColor = "#000000"
        };

        private AnimalResponseDto GetSampleAnimalResponse() => new(
            Guid.NewGuid(),
            "test1",
            5.0,
            AnimalCategory.Cat,
            "#000000"
        );

        private Animal GetSampleAnimalModel() => new()
        {
            Id = Guid.NewGuid(),
            Name = "test1",
            Weight = 5.0,
            Category = AnimalCategory.Cat,
            FurColor = Color.Black
        };
    }
}
