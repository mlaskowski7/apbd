using System.Drawing;
using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Models;

namespace VetClinicShelterApi.Mappers
{
    public class AnimalMapper : IAnimalMapper
    {
        public AnimalResponseDto MapToContract(Animal model)
        {
            var colorAsHex = $"#{model.FurColor.R:X2}{model.FurColor.G:X2}{model.FurColor.B:X2}";
            return new(
                model.Id, 
                model.Name, 
                model.Weight, 
                model.Category, 
                colorAsHex);
        }

        public Animal MapToModel(AnimalRequestDto requestDto)
        {
            var parsedColor = ColorTranslator.FromHtml(requestDto.FurColor);
            return new()
            {
                Name = requestDto.Name,
                Weight = requestDto.Weight,
                Category = requestDto.Category,
                FurColor = parsedColor
            };
        }
    }
}
