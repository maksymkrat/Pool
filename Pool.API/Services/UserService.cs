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

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<UserAccount> GetUserAccountByEmail(string email)
    {
        return _userRepository.GetUserAccountByEmail(email);
    }

    public async Task<bool> CreateUser(RegistrationModel newUser)
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

    public  string EncryptPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return "";
        password += KEY;
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(passwordBytes);

    }

    public  string DecryptPassword(string base64EncodeDate)
    {
        if (string.IsNullOrEmpty(base64EncodeDate)) return "";
        var base64EncodeBytes = Convert.FromBase64String(base64EncodeDate);
        var result = Encoding.UTF8.GetString(base64EncodeBytes);
        result = result.Substring(0, result.Length - KEY.Length);
        return result;
    }
}