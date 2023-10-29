using Pool.API.Models;
using Pool.API.Repository.IRepository;

namespace Pool.API.Repository.StubRepository;

public class StubUserRepository : IUserRepository
{
    private List<UserAccount> _userAccounts;
    public StubUserRepository()
    {
        _userAccounts = new List<UserAccount>();
        _userAccounts.Add(new UserAccount()
        {
            Email = "max@gmail.com",
            FirstName = "Max",
            Id = new Guid("593f5afd-e9c9-4a18-8f0b-ac09797da7f4"),
            LastName = "Krat",
            PasswordHash = "cGFzc3NvbWVfc2VjcmV0X2tleQ==",
            PhoneNumber = "09838388383",
            Role = "USER",
            Username = "player"
        });
        _userAccounts.Add(new UserAccount()
        {
            Email = "bob@gmail.com",
            FirstName = "Bob",
            Id = new Guid("6f8c1916-d98c-4c89-8e18-a68a69bf2294"),
            LastName = "jos",
            PasswordHash = "cGFzc3NvbWVfc2VjcmV0X2tleQ==",
            PhoneNumber = "0878388383",
            Role = "USER",
            Username = "bob123"
        });
    }

    public async Task<UserAccount> GetUserAccountByEmail(string email)
    {
        return _userAccounts.First((account => account.Email.Equals(email)));
    }

    public async Task<bool> CreateUser(UserAccount user)
    {
        return true;
    }
}