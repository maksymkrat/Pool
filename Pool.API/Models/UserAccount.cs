namespace Pool.API.Models;

public class UserAccount
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is UserAccount account)
        {
            if (Id == account.Id &&
                Email == account.Email &&
                FirstName == account.FirstName &&
                LastName == account.LastName &&
                Username == account.Username &&
                PhoneNumber == account.PhoneNumber &&
                PasswordHash == account.PasswordHash &&
                Role == account.Role)
            {
                return true;
            }
            
        }

        return false;
    }

    public override int GetHashCode()
    {
        return string.Format("{0}_{1}_{2}", Id, Email, Username).GetHashCode();
    }
}