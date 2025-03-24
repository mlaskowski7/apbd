using tutorial2.Exceptions;
using tutorial2.Models;

namespace tutorial2.Tests.Models;

[TestFixture]
[TestOf(typeof(LiquidContainer))]
public class LiquidContainerTest
{

    private LiquidContainer testContainer;
    
    [SetUp]
    public void Setup()
    {
        testContainer = new LiquidContainer(
            height: 200, 
            tareWeight: 1000, 
            cargoWeight: 0, 
            depth: 500, 
            maxPayload: 5000);
    }
    
    [Test]
    public void SerialNumber_ReturnsAccordingToContainersTypeCharacter()
    {
        // arrange
        string expectedSerialNumber = "KON-L-";
        
        // act
        string actualSerialNumber = testContainer.SerialNumber;
        
        // assert
        StringAssert.StartsWith(expectedSerialNumber, actualSerialNumber);
    }
    
    [Test]
    public void CargoMass_ReturnsSumOfCargoWeightAndTareContainerWeight()
    {
        // arrange
        testContainer.LoadContainer(500, false);

        // act
        double cargoMass = testContainer.CargoMass;

        // assert
        Assert.That(cargoMass, Is.EqualTo(1500) );
    }
    
    [Test]
    public void LoadContainer_ShouldAddToCargoWeight()
    {
        // act
        testContainer.LoadContainer(1000, false);

        // assert
        Assert.That(testContainer.CargoWeight, Is.EqualTo(1000));
    }

    [Test]
    public void LoadContainer_WhenHazardousCargoIsOverfillingByHalf_ThrowsDangerousOperationException()
    {
        // assert
        Assert.Throws<DangerousOperationException>(() => testContainer.LoadContainer(3000, true));
    }

    [Test]
    public void LoadContainer_InHazardousWhenNonHazardousCargoIsOverfillingByPoint9_ThrowsDangerousOperationException()
    {
        // assert
        Assert.Throws<DangerousOperationException>(() => testContainer.LoadContainer(4600, false));
    }

    [Test]
    public void EmptyCargo_ShouldMakeCargoWeightZeroed()
    {
        // arrange
        testContainer.LoadContainer(1500, false);

        // act
        testContainer.EmptyTheCargo();

        // assert
        Assert.That(testContainer.CargoWeight, Is.EqualTo(0));
    }
}