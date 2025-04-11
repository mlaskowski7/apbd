namespace VetClinicShelterApi.Utils
{
    public class ResultWrapperBase
    {
        public bool IsOk { get; protected set; }
        public string? ErrorMessage { get; protected set; }
    }
}
