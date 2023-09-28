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
}