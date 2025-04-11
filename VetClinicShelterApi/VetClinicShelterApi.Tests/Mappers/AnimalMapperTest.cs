using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Mappers;
using VetClinicShelterApi.Models;

namespace VetClinicShelterApi.Tests.Mappers
{
    public class AnimalMapperTest
    {
        private readonly AnimalMapper _mapper;

        public AnimalMapperTest()
        {
            _mapper = new AnimalMapper();
        }

        [Theory]
        [InlineData("#FFFFFF", 255, 255, 255)]
        [InlineData("#000000", 0, 0, 0)]
        [InlineData("#ABCDEF", 171, 205, 239)]
        public void MapToModel_ShouldCorrectlyParseColor(string hex, int r, int g, int b)
        {
            var dto = new AnimalRequestDto
            {
                Name = "ColorTest",
                Weight = 1.0,
                Category = AnimalCategory.Dog,
                FurColor = hex
            };

            var result = _mapper.MapToModel(dto);
            var animal = result.Result!;

            Assert.Multiple(() =>
            {
                Assert.Equal(r, animal.FurColor.R);
                Assert.Equal(g, animal.FurColor.G);
                Assert.Equal(b, animal.FurColor.B);
            });
        }
    }
}
