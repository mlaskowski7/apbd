using tutorial2.Interfaces;

namespace tutorial2.Models;

public class GasContainer : HazardousContainer
{
    public GasContainer(
        double height, 
        double tareWeight, 
        double cargoWeight, 
        double depth, 
        double maxPayload, 
        double pressure) : base(height, tareWeight, cargoWeight, depth, maxPayload, 'G')
    {
        this.Pressure = pressure > 0.0 ? pressure : throw new ArgumentOutOfRangeException(nameof(pressure), pressure, "pressure must be greater than zero");
    }

    public double Pressure { get; }
    
    public override void EmptyTheCargo()
    {
        this.CargoWeight *= 0.05;
    }

    public override string ToString()
    {
        return base.ToString() + $"\nPressure={this.Pressure}";
    }

    /// <summary>
    /// proxy method for loading the container.
    /// </summary>
    /// <param name="massOfCargo">massOfCargo.</param>
    public new void LoadContainer(double massOfCargo)
    {
        base.LoadContainer(massOfCargo);
    }
}