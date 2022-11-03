namespace Pool.API.Models;

public class UserAccount
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordH { get; set; }
    public string Role { get; set; }
}