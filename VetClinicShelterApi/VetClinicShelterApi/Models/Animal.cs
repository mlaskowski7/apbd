using System.Drawing;

namespace VetClinicShelterApi.Models;

public class Animal
{
    public Animal(string name, AnimalCategory category, double weight, Color furColor)
    {
        Id = Guid.NewGuid();
        Name = name;
        Category = category;
        Weight = weight;
        FurColor = furColor;
    }

    public Animal()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; private set; }
    
    public required string Name { get; set; }
    
    public double Weight { get; set; }
    
    public AnimalCategory Category { get; set; }
    
    public Color FurColor { get; set; }
}