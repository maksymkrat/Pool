namespace Pool.Shared.Models;

public class UserSession
{
    public string UserName { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    public int ExpiryIn { get; set; }
    public DateTime ExpiryTimeStamp { get; set; }
}