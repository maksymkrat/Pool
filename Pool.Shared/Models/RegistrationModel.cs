using System.ComponentModel.DataAnnotations;

namespace Pool.Shared.Models;

public class RegistrationModel
{
    [Required(ErrorMessage = "FirstName email")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "LastName email")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Username email")]
    public string Username { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "Enter password")]
    [StringLength(100, ErrorMessage = "min 4 symbols", MinimumLength = 4)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Confirm Password is required")]
    [StringLength(100, ErrorMessage = "Must be between 4 and 100 characters", MinimumLength = 4)]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string PasswordConfirm { get; set; }
}