using Pool.API.Models;

namespace Pool.API.Services.IServicec;

public interface IUserService
{
    Task<UserAccount> GetUserAccountByEmail(string email);
    public string EncryptPassword(string password);
    public string DecryptPassword(string base64EncodeDate);
}