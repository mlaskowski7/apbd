using VetClinicShelterApi.Utils;

namespace VetClinicShelterApi.Mappers
{
    /// <summary>
    /// interface for mappers from models to contract objects.
    /// </summary>
    /// <typeparam name="T">request dto type.</typeparam>
    /// <typeparam name="R">response dto type.</typeparam>
    /// <typeparam name="M">model type.</typeparam>
    public interface IContractMapper<T, R, M>
    {
        R MapToContract(M model);

        ResultWrapper<M> MapToModel(T requestDto);
    }
}
