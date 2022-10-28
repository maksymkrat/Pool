using Pool.API.Models;

namespace Pool.API.Repository;

public interface IUserRepository
{
    UserAccount GetUserAccountByEmail(string email);
}