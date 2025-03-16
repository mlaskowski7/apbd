using System.Text;
using tutorial2.Exceptions;

namespace tutorial2.Models;

public class Ship
{
    
    public Ship(IEnumerable<Container>? containers, int maxSpeed, int maxContainers, double maxWeight)
    {
        this.Containers = containers ?? new List<Container>();
        this.MaxSpeed = maxSpeed;
        this.MaxContainers = maxContainers;
        this.MaxWeight = maxWeight;
    }

    public IEnumerable<Container> Containers { get; set; }
    
    public int MaxSpeed { get; set; }
    
    public int MaxContainers { get; set; }
    
    public double MaxWeight { get; set; }

    public void LoadContainer(Container container)
    {
        if (Containers.Count() + 1 > MaxContainers)
        {
            throw new ShipOverloadException(
                $"Cannot add another container as the maximum number of containers will be exceeded - Max = {MaxContainers}, already holding {Containers.Count()} containers");
        }

        if (this.GetAllContainersWeight() + container.CargoMass > MaxWeight * 1000)
        {
            throw new ShipOverloadException(
                $"Cannot add another container as the maximum ship weight will be exceeded");
        }
        
        this.Containers = this.Containers.Append(container);
    }
    
    public void LoadContainer(IEnumerable<Container> containers)
    {
        if (Containers.Count() + containers.Count() > MaxContainers)
        {
            throw new ShipOverloadException(
                $"Cannot add another container as the maximum number of containers will be exceeded - Max = {MaxContainers}, already holding {Containers.Count()} containers");
        }

        if (this.GetAllContainersWeight() + containers.Select(cont => cont.CargoMass).Sum() > MaxWeight * 1000)
        {
            throw new ShipOverloadException(
                $"Cannot add another container as the maximum ship weight will be exceeded");
        }
        
        this.Containers = this.Containers.Concat(containers);
    }

    public void RemoveContainer(Container container)
    {
        if (!Containers.Contains(container))
        {
            throw new InvalidOperationException("The container to remove was not found on the ship.");
        }
    
        this.Containers = this.Containers.Where(c => c != container);
    }

    public void ReplaceContainerBySerialNumber(string serialNumber, Container container)
    {
        if (Containers.All(c => c.SerialNumber != serialNumber))
        {
            throw new InvalidOperationException($"The container with given serial number ({serialNumber}) was not found on the ship.");
        } 
        
        this.Containers = Containers.Where(c => c.SerialNumber != serialNumber)
                                    .Append(container);
    }

    public void TransferContainerToAnotherShip(Container container, Ship ship)
    {
        if (!Containers.Contains(container))
        {
            throw new InvalidOperationException("The container to transfer was not found on the ship.");
        }
        
        this.RemoveContainer(container);
        ship.LoadContainer(container);
    }

    private double GetAllContainersWeight()
    {
        return this.Containers.Select(cont => cont.CargoMass)
                              .Sum();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Ship Details:");
        sb.AppendLine($"Max Speed={MaxSpeed} knots");
        sb.AppendLine($"Max Containers={MaxContainers}");
        sb.AppendLine($"Max Weight={MaxWeight} kg");
        sb.AppendLine($"Current Containers={Containers.Count()}");
        foreach (var container in Containers)
        {
            sb.AppendLine($"{container.ToString()}");
        }

        return sb.ToString();
    }
}