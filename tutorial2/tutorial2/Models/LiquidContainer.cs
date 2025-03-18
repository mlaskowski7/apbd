using tutorial2.Exceptions;
using tutorial2.Interfaces;

namespace tutorial2.Models;

public class LiquidContainer : Container, IHazardNotifier
{
    public LiquidContainer(
        double height, 
        double tareWeight, 
        double cargoWeight, 
        double depth, 
        double maxPayload) : base(height, tareWeight, cargoWeight, depth, maxPayload)
    {
    }

    protected override char GetTypeForSerialNumber() => 'L';
    
    public void NotifyAboutHazardousSituation()
    {
        Console.WriteLine($"[DANGER] A hazardous situation occured in container {this.SerialNumber}");
    }
    
    public void LoadContainer(double massOfCargo, bool isHazardous)
    {
        if (massOfCargo <= 0)
        {
            throw new ArgumentException("massOfCargo cannot be negative or zero");
        }
        
        if (isHazardous && this.CargoWeight + massOfCargo > this.MaxPayload * 0.5)
        {
            throw new DangerousOperationException(
                $"Mass of hazardous cargo can not be more then half of max payload. Max payload was {this.MaxPayload} and attempted to add {massOfCargo} kg of hazardous cargo, when {this.CargoWeight} is already there");
        }

        if (!isHazardous && this.CargoWeight + massOfCargo > this.MaxPayload * 0.9)
        {
            throw new DangerousOperationException(
                $"Mass of liquid cargo can not be more then 0.9 of max payload. Max payload was {this.MaxPayload} and attempted to add {massOfCargo} kg of liquid cargo, when {this.CargoWeight} is already there");
        }
        
        this.LoadContainer(massOfCargo);
    }
    
    
}