using Pool.API.Authentication;
using Pool.API.Models;

namespace Pool.API.Services;

public class UserAccountService
{
    private List<UserAccount> _userAccountList;

    public UserAccountService()
    {
        _userAccountList = new List<UserAccount>()
        {
            new UserAccount(){Email = "admin", PasswordH= "admin", Role = "ADMIN"},
            new UserAccount(){Email = "user", PasswordH= "user", Role = "USER"}
        };
    }

    public UserAccount? GetUserAccountByUserName(string userName)
    {
        return _userAccountList.FirstOrDefault(x => x.Email == userName);
    }
}