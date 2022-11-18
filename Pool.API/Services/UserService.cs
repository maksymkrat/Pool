using System.Text;
using Pool.API.Models;
using Pool.API.Repository.IRepository;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;

namespace Pool.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private const string KEY = "some_secret_key";
    private readonly ILogger _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public Task<UserAccount> GetUserAccountByEmail(string email)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: GetUserAccountByEmail");
        return _userRepository.GetUserAccountByEmail(email);
    }

    public async Task<bool> CreateUser(RegistrationModel newUser)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: CreateUser");
        try
        {
            var userAccount = new UserAccount()
            {
                Id = Guid.NewGuid(),
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Username = newUser.Username,
                PhoneNumber = newUser.Phone,
                PasswordH = EncryptPassword(newUser.PasswordConfirm)
            };
            var result = await _userRepository.CreateUser(userAccount);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string EncryptPassword(string password)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: EncryptPassword");
        try
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += KEY;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string DecryptPassword(string base64EncodeDate)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: DecryptPassword");
        try
        {
            if (string.IsNullOrEmpty(base64EncodeDate)) return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeDate);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - KEY.Length);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}