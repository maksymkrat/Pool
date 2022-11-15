using System.ComponentModel.DataAnnotations;

namespace Pool.Shared.Models;

public class LoginRequestModel
{
    [Required(ErrorMessage = "enter email")]
    [StringLength(100, ErrorMessage = "min 4 symbols", MinimumLength = 4)]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "enter password")]
    [StringLength(100, ErrorMessage = "min 4 symbols", MinimumLength = 4)]
    [DataType(DataType.Password)]
    public string Password { get; set; }    
}