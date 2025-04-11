using System.Drawing;

namespace VetClinicShelterApi.Models;

public class Animal
{
    public Animal(Guid id, string name, AnimalCategory category, decimal weight, Color furColor)
    {
        Id = id;
        Name = name;
        Category = category;
        Weight = weight;
        FurColor = furColor;
    }

    public Animal()
    {
        
    }
    
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public decimal Weight { get; set; }
    
    public AnimalCategory Category { get; set; }
    
    public Color FurColor { get; set; }
}