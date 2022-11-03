using Pool.API.Models;
using Pool.API.Repository.IRepository;
using Pool.API.Services.IServicec;

namespace Pool.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<UserAccount> GetUserAccountByEmail(string email)
    {
        return _userRepository.GetUserAccountByEmail(email);
    }
}