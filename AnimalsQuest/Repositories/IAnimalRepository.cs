using AnimalsQuest.Models;

namespace AnimalsQuest.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    Animal GetAnimal(int id);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int id);
}