using VetClinicShelterApi.Models;

namespace VetClinicShelterApi.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly Dictionary<Guid, Animal> _animals;

        public AnimalRepository(Dictionary<Guid, Animal>? animals = null)
        {
            _animals = animals ?? [];
        }

        public bool DeleteAnimalById(Guid animalId)
        {
            return _animals.Remove(animalId);
        }

        public ICollection<Animal> FindAllAnimals()
        {
            return _animals.Values
                           .ToList();
        }

        public Animal? FindAnimalById(Guid id)
        {
            _animals.TryGetValue(id, out var animal);
            return animal;
        }

        public void SaveAnimal(Animal animal)
        {
            _animals[animal.Id] = animal;
        }
    }
}
