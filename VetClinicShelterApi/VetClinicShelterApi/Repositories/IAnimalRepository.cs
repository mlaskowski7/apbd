using VetClinicShelterApi.Models;

namespace VetClinicShelterApi.Repositories
{
    public interface IAnimalRepository
    {
        void SaveAnimal(Animal animal);

        ICollection<Animal> FindAllAnimals();

        Animal? FindAnimalById(Guid id);

        bool DeleteAnimalById(Guid animalId);
    }
}
