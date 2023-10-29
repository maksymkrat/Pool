using Microsoft.Extensions.Logging;
using Moq;
using Pool.API.Models;
using Pool.API.Repository.IRepository;
using Pool.API.Repository.StubRepository;
using Pool.API.Services;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;

namespace Pool.APITests.Services;

[TestClass]
public class UserServiceTests
{
    
    public TestContext TestContext { get; set; }
    
    private static  IUserService _userService;
    private static IUserRepository _userRepository;
    private static Mock<ILogger<UserService>> loggerMock = new Mock<ILogger<UserService>>();
    private static Mock<IUserRepository> usrRepoMock = new Mock<IUserRepository>();

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        _userRepository = new StubUserRepository();
        _userService = new UserService(_userRepository,loggerMock.Object);
    }

    [TestMethod]
    public void EncryptPassword_InputPass_ReturnEncryptedPass()
    {
        //arrange
        string input = "pass";
        string expected = "cGFzc3NvbWVfc2VjcmV0X2tleQ==";
        //act
        string actual = _userService.EncryptPassword(input);
        
        //asserts
        Assert.AreEqual(expected,actual);
    }
    
    [TestMethod]
    public void DecryptPassword_InputPasswordHash_ReturnPass()
    {
        //arrange
        string  input = "cGFzc3NvbWVfc2VjcmV0X2tleQ==";
        string expected = "pass";
        //act
        string actual = _userService.DecryptPassword(input);
        
        //asserts
        Assert.AreEqual(expected,actual);
    }

    [TestMethod]
    public void GetUserAccountByEmail_InputEmail_ReturnUserAccount()
    {
        //arrange
        string email = "bob@gmail.com";
        UserAccount expected = new UserAccount()
        {
            Email = "bob@gmail.com",
            FirstName = "Bob",
            Id = new Guid("6f8c1916-d98c-4c89-8e18-a68a69bf2294"),
            LastName = "jos",
            PasswordHash = "cGFzc3NvbWVfc2VjcmV0X2tleQ==",
            PhoneNumber = "0878388383",
            Role = "USER",
            Username = "bob123"
        };
        
        //act
        UserAccount actual = _userService.GetUserAccountByEmail(email).Result;
        //asserts
        Assert.AreEqual(expected,actual, "somme message");

    }

    [TestMethod]
    public async  Task CreateUser_InputUserModel_ReturnTrue()
    {
        //arrange

        RegistrationModel input = new RegistrationModel()
        {
            Email = "bob@gmail.com",
            FirstName = "Bob",
            LastName = "jos",
            Username = "bob123",
            Phone = "0878388383",
            Password = "pass",
            PasswordConfirm = "pass",
           
        };
        _userService = new UserService(usrRepoMock.Object,loggerMock.Object);

         usrRepoMock.Setup(x  => x.CreateUser(It.IsAny<UserAccount>()))
            .ReturnsAsync(true);
        //act
        var actual = await _userService.CreateUser(input);

        //asserts
        Assert.IsTrue(actual);
    }
    
    [TestMethod]
    public async  Task CreateUser_InputNull_ReturnFalse()
    {
        //arrange

        _userService = new UserService(usrRepoMock.Object,loggerMock.Object);

        //act
        var actual = await _userService.CreateUser(null);

        //asserts
        Assert.IsFalse(actual);
    }
}

