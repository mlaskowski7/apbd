using tutorial2.Interfaces;

namespace tutorial2.Models;

public class GasContainer : Container, IHazardNotifier
{
    public GasContainer(
        double height, 
        double tareWeight, 
        double cargoWeight, 
        double depth, 
        double maxPayload, 
        double pressure) : base(height, tareWeight, cargoWeight, depth, maxPayload)
    {
        this.Pressure = pressure > 0.0 ? pressure : throw new ArgumentOutOfRangeException(nameof(pressure), pressure, "pressure must be greater than zero");
    }

    public double Pressure { get; set; }
    
    protected override char GetTypeForSerialNumber() => 'G';
    
    public void NotifyAboutHazardousSituation()
    {
        Console.WriteLine($"[DANGER] A hazardous situation occured in container {this.SerialNumber}");
    }
    
    public override void EmptyTheCargo()
    {
        this.CargoWeight *= 0.05;
    }

    public override string ToString()
    {
        return base.ToString() + $"\nPressure={this.Pressure}";
    }
}