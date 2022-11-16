using Pool.API.Models;
using Pool.Shared.Models;

namespace Pool.API.Services.IServicec;

public interface IUserService
{
    Task<UserAccount> GetUserAccountByEmail(string email);
    Task<bool> CreateUser(RegistrationModel newUser);
    public string EncryptPassword(string password);
    public string DecryptPassword(string base64EncodeDate);
}