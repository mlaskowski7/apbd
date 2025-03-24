using tutorial2.Exceptions;

namespace tutorial2.Models;

public class RefrigeratedContainer : Container
{
    public RefrigeratedContainer(
        double height, 
        double tareWeight, 
        double cargoWeight, 
        double depth, 
        double maxPayload, 
        string productType, 
        double temperature) : base(height, tareWeight, cargoWeight, depth, maxPayload, 'C')
    {
        this.ProductType = string.IsNullOrEmpty(productType) ? throw new ArgumentNullException(nameof(productType)) : productType;
        this.Temperature = temperature;
    }

    public string ProductType { get; }
    
    public double Temperature { get; }

    public void LoadContainer(double massOfCargo, string productType, double minTemperature)
    {
        if (massOfCargo <= 0)
        {
            throw new ArgumentException("massOfCargo cannot be negative or zero");
        }
        
        if (!productType.Equals(this.ProductType))
        {
            throw new ProductTypeNotAllowedException(
                $"The product type '{productType}' is not supported as this container type is '${this.ProductType}'.");
        }

        if (this.Temperature < minTemperature)
        {
            throw new TemperatureNotMatchingException(
                $"The container temperature ({this.Temperature}) is less than required for this product - {minTemperature}.");
        }
        
        this.LoadContainer(massOfCargo);
    }

    public override string ToString()
    {
        return base.ToString() + $"\nProductType={this.ProductType}\nTemperature={this.Temperature}";
    }
}