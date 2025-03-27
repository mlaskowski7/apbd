using LegacyApp;
using LegacyApp.DataAccessLayer;
using LegacyApp.DateTimeProviders;
using LegacyApp.Models;
using Moq;

namespace TestProject1;

public class UserServiceTest
{
    private readonly UserService _userService;
    
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
    
    private readonly Mock<IUserCreditService> _userCreditServiceMock;
    
    public UserServiceTest()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _userCreditServiceMock = new Mock<IUserCreditService>();
        _userService = new UserService
        (
            _dateTimeProviderMock.Object, 
            _clientRepositoryMock.Object, 
            _userCreditServiceMock.Object
        );
    }

    [Theory]
    [InlineData("", "test")]
    [InlineData("test", "")]
    public void AddUser_WhenFirstOrLastNameEmpty_ShouldReturnFalse(string firstName, string lastName)
    {
        // arrange
        var email = "test@test.com";
        var dateOfBirth = DateTime.Today.AddDays(-10);
        var clientId = 1;

        // act
        var result =  _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // assert
        Assert.False(result);
    }
    
    [Theory]
    [InlineData("testg.")]
    [InlineData("test@sdsa")]
    public void AddUser_WhenEmailIsInvalid_ShouldReturnFalse(string email)
    {
        // arrange
        var firstName = "test";
        var lastName = "test";
        var dateOfBirth = DateTime.Today.AddDays(-10);
        var clientId = 1;

        // act
        var result =  _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_WhenAgeIsNotMoreThan21_ShouldReturnFalse()
    {
        // arrange
        var firstName = "test";
        var lastName = "test";
        var email = "test@test.com";
        var dateOfBirth = DateTime.Today.AddDays(-10);
        var clientId = 1;
        
        _dateTimeProviderMock.Setup(x => x.Now)
                             .Returns(DateTime.Today.AddYears(1));

        // act
        var result =  _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_WhenCreditLimitIsLessThan500OnNormalUser_ShouldReturnFalse()
    {
        // arrange
        var firstName = "test";
        var lastName = "test";
        var email = "test@test.com";
        var dateOfBirth = DateTime.Today.AddDays(-10);
        var clientId = 1;
        var client = new Client("test", 1, "test@gmail.com", "warsaw", "NormalClient");
        
        _dateTimeProviderMock.Setup(dateTimeProvider => dateTimeProvider.Now)
                             .Returns(DateTime.Today.AddYears(21));
        _clientRepositoryMock.Setup(repo => repo.GetById(clientId))
                             .Returns(client);
        _userCreditServiceMock.Setup(service => service.GetCreditLimit(lastName, dateOfBirth))
                              .Returns(400);

        // act
        var result =  _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_WhenCreditLimitIsLessThan500OnVeryImportantClient_ShouldReturnTrue()
    {
        // arrange
        var firstName = "test";
        var lastName = "test";
        var email = "test@test.com";
        var dateOfBirth = DateTime.Today.AddDays(-10);
        var clientId = 1;
        var client = new Client("test", 1, "test@gmail.com", "warsaw", "VeryImportantClient");
        
        _dateTimeProviderMock.Setup(dateTimeProvider => dateTimeProvider.Now)
                             .Returns(DateTime.Today.AddYears(21));
        _clientRepositoryMock.Setup(repo => repo.GetById(clientId))
                             .Returns(client);
        _userCreditServiceMock.Setup(service => service.GetCreditLimit(lastName, dateOfBirth))
                              .Returns(400);

        // act
        var result =  _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // assert
        Assert.True(result);
    }
    
    [Fact]
    public void AddUser_WhenIsImportantClient_ShouldDoubleTheLimitAndReturnTrue()
    {
        // arrange
        var firstName = "test";
        var lastName = "test";
        var email = "test@test.com";
        var dateOfBirth = DateTime.Today.AddDays(-10);
        var clientId = 1;
        var client = new Client("test", 1, "test@gmail.com", "warsaw", "ImportantClient");
        
        _dateTimeProviderMock.Setup(dateTimeProvider => dateTimeProvider.Now)
                             .Returns(DateTime.Today.AddYears(21));
        _clientRepositoryMock.Setup(repo => repo.GetById(clientId))
                             .Returns(client);
        _userCreditServiceMock.Setup(service => service.GetCreditLimit(lastName, dateOfBirth))
                              .Returns(400);

        // act
        var result =  _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // assert
        Assert.True(result);
    }
}