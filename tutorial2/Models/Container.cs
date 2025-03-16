using System.Text;
using tutorial2.Exceptions;

namespace tutorial2.Models;

public abstract class Container
{
    private static readonly string SerialNumberPrefix = "KON";
    private static readonly string SerialNumberSeparator = "-";
    
    private static int _uniqueCounter;
    private readonly int _uniqueNumber;

    protected Container(
        double height, 
        double tareWeight, 
        double cargoWeight, 
        double depth, 
        double maxPayload)
    {
        this._uniqueNumber = _uniqueCounter;
        _uniqueCounter++;
        
        this.Height = height;
        this.TareWeight = tareWeight;
        this.CargoWeight = cargoWeight;
        this.Depth = depth;
        this.MaxPayload = maxPayload;
    }

    /// <summary>
    /// The mass of the cargo (in kilograms).
    /// </summary>
    public double CargoMass => this.CargoWeight + this.TareWeight;

    /// <summary>
    /// Height (in centimeters).
    /// </summary>
    public double Height { get; set; }
    
    /// <summary>
    /// Weight of the container itself, in kilograms.
    /// </summary>
    public double TareWeight { get; set; }
    
    /// <summary>
    /// Weight of the cargo itself.
    /// </summary>
    public double CargoWeight { get; set; }
    
    /// <summary>
    /// Depth, in centimeters.
    /// </summary>
    public double Depth { get; set; }
    
    /// <summary>
    /// Maximum payload in kilograms.
    /// </summary>
    public double MaxPayload { get; set; }

    public string SerialNumber
    {
        get
        {
            var sb = new StringBuilder(SerialNumberPrefix);
            sb.Append(SerialNumberSeparator);
            sb.Append(this.GetTypeForSerialNumber());
            sb.Append(SerialNumberSeparator);
            sb.Append(this._uniqueNumber);
            return sb.ToString();
        }
    }

    protected abstract char GetTypeForSerialNumber();

    public virtual void EmptyTheCargo()
    {
        this.CargoWeight = 0;
    }

    public void LoadContainer(double massOfCargo)
    {
        if (this.CargoWeight + massOfCargo > this.MaxPayload)
        {
            throw new OverfillException(
                $"Cannot load this mass of cargo {massOfCargo}, maximum payload is {this.MaxPayload} and there is already {this.CargoWeight} loaded");
        }
        
        this.CargoWeight += massOfCargo;
    }
}