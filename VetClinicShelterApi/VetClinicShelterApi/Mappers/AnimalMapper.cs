using System.Drawing;
using VetClinicShelterApi.Dtos.Request;
using VetClinicShelterApi.Dtos.Response;
using VetClinicShelterApi.Models;
using VetClinicShelterApi.Utils;

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

        public ResultWrapper<Animal> MapToModel(AnimalRequestDto requestDto)
        {
            var colorParseResult = parseColor(requestDto.FurColor);
            if (!colorParseResult.IsOk)
            {
                return ResultWrapper<Animal>.FromErr(colorParseResult);
            }
            
            var animal = new Animal()
            {
                Name = requestDto.Name,
                Weight = requestDto.Weight,
                Category = requestDto.Category,
                FurColor = colorParseResult.Result
            };
            return ResultWrapper<Animal>.Ok(animal);
        }

        private ResultWrapper<Color> parseColor(string colorAsHexStr)
        {
            try
            {
                var parsedColor = ColorTranslator.FromHtml(colorAsHexStr);

                return parsedColor.IsEmpty ? 
                    ResultWrapper<Color>.Err("Could not parse the passed color") : 
                    ResultWrapper<Color>.Ok(parsedColor);
            }
            catch (ArgumentException ex)
            {
                return ResultWrapper<Color>.Err("Could not parse the passed color -> " + ex.Message);
            }
        }
    }
}
