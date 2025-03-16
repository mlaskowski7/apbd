using System.Text;
using tutorial2.Exceptions;

namespace tutorial2.Models;

public abstract class Container
{
    private static readonly string SerialNumberPrefix = "KON";
    private static readonly string SerialNumberSeparator = "-";
    
    private static int uniqueCounter;
    private int uniqueNumber;

    protected Container()
    {
        this.uniqueNumber = uniqueCounter;
        uniqueCounter++;
    }

    /// <summary>
    /// The mass of the cargo (in kilograms).
    /// </summary>
    public decimal CargoMass => this.CargoWeight + this.TareWeight;

    /// <summary>
    /// Height (in centimeters).
    /// </summary>
    public decimal Height { get; set; }
    
    /// <summary>
    /// Weight of the container itself, in kilograms.
    /// </summary>
    public decimal TareWeight { get; set; }
    
    /// <summary>
    /// Weight of the cargo itself.
    /// </summary>
    public decimal CargoWeight { get; set; }
    
    /// <summary>
    /// Depth, in centimeters.
    /// </summary>
    public decimal Depth { get; set; }
    
    /// <summary>
    /// Maximum payload in kilograms.
    /// </summary>
    public decimal MaxPayload { get; set; }

    public string SerialNumber
    {
        get
        {
            var sb = new StringBuilder(SerialNumberPrefix);
            sb.Append(SerialNumberSeparator);
            sb.Append(this.GetTypeForSerialNumber());
            sb.Append(SerialNumberSeparator);
            sb.Append(this.uniqueNumber);
            return sb.ToString();
        }
    }

    protected abstract char GetTypeForSerialNumber();

    public virtual void EmptyTheCargo()
    {
        this.CargoWeight = 0;
    }

    public virtual void LoadContainer(decimal massOfCargo)
    {
        if (this.CargoWeight + massOfCargo > this.MaxPayload)
        {
            throw new OverfillException(
                $"Cannot load this mass of cargo {massOfCargo}, maximum payload is {this.MaxPayload} and there is already {this.CargoWeight} loaded");
        }
        
        this.CargoWeight += massOfCargo;
    }
}