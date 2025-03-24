using System.Text;
using tutorial2.Exceptions;

namespace tutorial2.Models;

public abstract class Container
{
    private static readonly string SerialNumberPrefix = "KON";
    private static readonly string SerialNumberSeparator = "-";
    
    private static int _uniqueCounter;

    protected Container(
        double height, 
        double tareWeight, 
        double cargoWeight, 
        double depth, 
        double maxPayload,
        char typeCharacter)
    {
        this.Height = height > 0 ? height : throw new ArgumentOutOfRangeException(nameof(height));
        this.TareWeight = tareWeight > 0 ? tareWeight : throw new ArgumentOutOfRangeException(nameof(tareWeight));
        this.CargoWeight = cargoWeight <= maxPayload ? cargoWeight : throw new ArgumentOutOfRangeException(nameof(cargoWeight));
        this.Depth = depth > 0 ? depth : throw new ArgumentOutOfRangeException(nameof(depth));
        this.MaxPayload = maxPayload > 0 ? maxPayload : throw new ArgumentOutOfRangeException(nameof(maxPayload));
        
        TypeCharacter = typeCharacter;
        this.SerialNumber = this.GenerateSerialNumber();
    }

    /// <summary>
    /// The mass of the cargo (in kilograms).
    /// </summary>
    public double CargoMass => this.CargoWeight + this.TareWeight;

    /// <summary>
    /// Height (in centimeters).
    /// </summary>
    public double Height { get; }
    
    /// <summary>
    /// Weight of the container itself, in kilograms.
    /// </summary>
    public double TareWeight { get; }
    
    /// <summary>
    /// Weight of the cargo itself.
    /// </summary>
    public double CargoWeight { get; protected set; }
    
    /// <summary>
    /// Depth, in centimeters.
    /// </summary>
    public double Depth { get; }
    
    /// <summary>
    /// Maximum payload in kilograms.
    /// </summary>
    public double MaxPayload { get; }

    public string SerialNumber { get; }

    protected char TypeCharacter { get; }

    public virtual void EmptyTheCargo()
    {
        this.CargoWeight = 0;
    }

    protected void LoadContainer(double massOfCargo)
    {
        if (massOfCargo <= 0)
        {
            throw new ArgumentException("massOfCargo cannot be negative or zero");
        }
        
        if (this.CargoWeight + massOfCargo > this.MaxPayload)
        {
            throw new OverfillException(
                $"Cannot load this mass of cargo {massOfCargo}, maximum payload is {this.MaxPayload} and there is already {this.CargoWeight} loaded");
        }

        this.CargoWeight += massOfCargo;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Container {SerialNumber}: ");
        sb.AppendLine($"Height={Height} cm, ");
        sb.AppendLine($"TareWeight={TareWeight} kg, ");
        sb.AppendLine($"CargoWeight={CargoWeight} kg, ");
        sb.AppendLine($"Depth={Depth} dm, ");
        sb.AppendLine($"MaxPayload={MaxPayload} kg, ");

        return sb.ToString();
    }

    private string GenerateSerialNumber()
    {
        var sb = new StringBuilder(SerialNumberPrefix);
        sb.Append(SerialNumberSeparator);
        sb.Append(TypeCharacter);
        sb.Append(SerialNumberSeparator);
        sb.Append(_uniqueCounter++);
        return sb.ToString();
    }
}