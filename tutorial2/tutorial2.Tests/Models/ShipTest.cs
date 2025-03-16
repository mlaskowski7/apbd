using NUnit.Framework;
using tutorial2.Exceptions;
using tutorial2.Models;

namespace tutorial2.Tests.Models;

[TestFixture]
[TestOf(typeof(Ship))]
public class ShipTest
{

    private Ship testShip;

    [SetUp]
    public void SetUp()
    {
        testShip = new Ship(null, 30, 5, 100000);
    }

    [Test]
    public void TestLoadContainer()
    {
        // arrange
        var container = new LiquidContainer(200, 100, 50, 250, 1500);

        // act
        testShip.LoadContainer(container);

        // assert
        Assert.That(testShip.Containers.Count(), Is.EqualTo(1));
    }

    [Test]
    public void TestLoadContainerOnOverload()
    {
        // arrange
        var ship = new Ship(null, 30, 1, 100000);
        var container1 = new LiquidContainer(200, 100, 50, 250, 1500);
        var container2 = new LiquidContainer(200, 100, 50, 250, 1500);

        // act
        ship.LoadContainer(container1);

        // assert
        Assert.Throws<ShipOverloadException>(() => ship.LoadContainer(container2));
    }

    [Test]
    public void TestLoadContainerWeightOverload()
    {
        // arrange
        var container1 = new LiquidContainer(200, 100, 50, 250, 1000);
        var container2 = new LiquidContainer(200, 100, 50, 250, 2000);
        var ship = new Ship(new List<Container>(), 30, 5, 0.2);

        // act
        ship.LoadContainer(container1);

        // assert
        Assert.Throws<ShipOverloadException>(() => ship.LoadContainer(container2));
    }

    [Test]
    public void TestRemoveContainer()
    {
        // arrange
        var container = new LiquidContainer(200, 100, 50, 250, 1500);
        testShip.LoadContainer(container);

        // act
        testShip.RemoveContainer(container);

        // assert
        Assert.That(testShip.Containers.Count(), Is.EqualTo(0));
    }

    [Test]
    public void TestRemoveContainerOnNotFound()
    {
        // assert
        Assert.Throws<InvalidOperationException>(() => testShip.RemoveContainer(new LiquidContainer(200, 100, 50, 250, 1500)));
    }

    [Test]
    public void TestReplaceContainer()
    {
        // arrange
        var container = new LiquidContainer(200, 100, 50, 250, 1500);
        testShip.LoadContainer(container);
        var newContainer = new LiquidContainer(250, 150, 75, 300, 2000);

        // act
        testShip.ReplaceContainerBySerialNumber(container.SerialNumber, newContainer);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(testShip.Containers.Count(), Is.EqualTo(1));
            Assert.That(testShip.Containers.First().SerialNumber, Is.EqualTo(newContainer.SerialNumber));
        });
    }

    [Test]
    public void TestReplaceContainerOnNotFound()
    {
        // assert
        Assert.Throws<InvalidOperationException>(() => testShip.ReplaceContainerBySerialNumber("dsadasdas", new LiquidContainer(250, 150, 75, 300, 2000)));
    }

    [Test]
    public void TestTransferContainer()
    {
        // arrange
        var container = new LiquidContainer(200, 100, 50, 250, 1500);
        var anotherShip = new Ship(new List<Container>(), 30, 5, 100000);
        testShip.LoadContainer(container);

        // act
        testShip.TransferContainerToAnotherShip(container, anotherShip);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(testShip.Containers.Count(), Is.EqualTo(0));
            Assert.That(anotherShip.Containers.Count(), Is.EqualTo(1));
        });
    }

    [Test]
    public void TestTransferContainerOnNotFound()
    {
        // arrange
        var container = new LiquidContainer(200, 100, 50, 250, 1500);
        var anotherShip = new Ship(new List<Container>(), 30, 5, 100000);

        // assert
        Assert.Throws<InvalidOperationException>(() => testShip.TransferContainerToAnotherShip(container, anotherShip));
    }
}