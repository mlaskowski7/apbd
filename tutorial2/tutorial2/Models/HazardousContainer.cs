using tutorial2.Interfaces;

namespace tutorial2.Models;

public abstract class HazardousContainer : Container, IHazardNotifier
{
    protected HazardousContainer(
        double height, 
        double tareWeight, 
        double cargoWeight, 
        double depth, 
        double maxPayload,
        char typeCharacter) : base(height, tareWeight, cargoWeight, depth, maxPayload, typeCharacter)
    {
    }

    public void NotifyAboutHazardousSituation()
    {
        Console.WriteLine($"[DANGER] A hazardous situation occured in container {this.SerialNumber}");
    }
}