using Pool.API.Models;

namespace Pool.API.Repository.IRepository;

public interface IUserRepository
{
    Task<UserAccount> GetUserAccountByEmail(string email);
}