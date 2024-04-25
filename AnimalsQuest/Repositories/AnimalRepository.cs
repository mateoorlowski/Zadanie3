using System.Data.SqlClient;
using AnimalsQuest.Models;

namespace AnimalsQuest.Repositories;

public class AnimalRepository(IConfiguration configuration) : IAnimalRepository
{
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        using var command = connection.CreateCommand();

        var orderByParameter = orderBy.ToLower() switch
        {
            "id" => "Id",
            "category" => "Category",
            "description" => "Description",
            "area" => "Area",
            _ => "Name"
        };

        command.CommandText = $"SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY {orderByParameter}";

        connection.Open();
        using var reader = command.ExecuteReader();
        var animals = new List<Animal>();

        while (reader.Read())
        {
            animals.Add(new Animal
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = (string)reader["Name"],
                Description = (string)reader["Description"],
                Category = (string)reader["Category"],
                Area = (string)reader["Area"]
            });
        }

        return animals;
    }

    public Animal GetAnimal(int id)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal WHERE IdAnimal = @IdAnimal";
        command.Parameters.AddWithValue("@IdAnimal", id);
        connection.Open();
        using var reader = command.ExecuteReader();

        Animal animal = null!;

        if (reader.Read())
        {
            animal = new Animal
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = (string)reader["Name"],
                Description = (string)reader["Description"],
                Category = (string)reader["Category"],
                Area = (string)reader["Area"]
            };
        }

        return animal;
    }

    public int CreateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        using var command = connection.CreateCommand();

        command.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area); SELECT SCOPE_IDENTITY()";
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);

        connection.Open();
        var id = command.ExecuteScalar();
        return Convert.ToInt32(id);
    }
    

    
    public int UpdateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        using var command = connection.CreateCommand();

        command.CommandText = "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @Id";
        command.Parameters.AddWithValue("@Id", animal.IdAnimal);
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);

        connection.Open();
        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }

    public int DeleteAnimal(int id)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        using var command = connection.CreateCommand();

        command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @Id";
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }
}