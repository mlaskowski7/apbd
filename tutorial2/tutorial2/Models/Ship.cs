using System.Collections;
using System.Text;
using tutorial2.Exceptions;

namespace tutorial2.Models;

public class Ship
{
    
    public Ship(
        int maxSpeed, 
        int maxContainers, 
        double maxWeight, 
        IList<Container>? containers = null
    )
    {
        this.Containers = containers ?? new List<Container>();
        this.MaxSpeed = maxSpeed;
        this.MaxContainers = maxContainers;
        this.MaxWeight = maxWeight;
    }

    public IList<Container> Containers { get; private set; }
    
    public int MaxSpeed { get; }
    
    public int MaxContainers { get; }
    
    public double MaxWeight { get; }

    public void LoadContainer(Container container)
    {
        if (Containers.Count() + 1 > MaxContainers)
        {
            throw new ShipOverloadException(
                $"Cannot add another container as the maximum number of containers will be exceeded - Max = {MaxContainers}, already holding {Containers.Count()} containers");
        }

        if (GetAllContainersWeight() + container.CargoMass > MaxWeight * 1000)
        {
            throw new ShipOverloadException(
                $"Cannot add another container as the maximum ship weight will be exceeded");
        }
        
        Containers = Containers.Append(container).ToList();
    }
    
    public void LoadContainer(IList<Container> containers)
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
        
        this.Containers = this.Containers.Concat(containers).ToList();
    }

    public void RemoveContainer(Container container)
    {
        if (!Containers.Contains(container))
        {
            throw new InvalidOperationException("The container to remove was not found on the ship.");
        }
    
        this.Containers = this.Containers.Where(c => c != container).ToList();
    }

    public void ReplaceContainerBySerialNumber(string serialNumber, Container container)
    {
        if (Containers.All(c => c.SerialNumber != serialNumber))
        {
            throw new InvalidOperationException($"The container with given serial number ({serialNumber}) was not found on the ship.");
        } 
        
        this.Containers = Containers.Where(c => c.SerialNumber != serialNumber)
                                    .Append(container)
                                    .ToList();
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