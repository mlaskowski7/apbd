namespace VetClinicShelterApi.Dtos.Response
{
    public record VisitResponseDto(DateTime DateOfVisit, AnimalResponseDto Animal, string Description, double Price)
    {
    }
}
