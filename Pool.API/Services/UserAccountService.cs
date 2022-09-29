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
            new UserAccount(){UserName = "admin", Password="admin", Role = "ADMIN"},
            new UserAccount(){UserName = "user", Password="user", Role = "USER"}
        };
    }

    public UserAccount? GetUserAccountByUserName(string userName)
    {
        return _userAccountList.FirstOrDefault(x => x.UserName == userName);
    }
}