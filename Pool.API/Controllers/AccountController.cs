using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pool.API.Authentication;
using Pool.API.Services;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;

namespace Pool.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController( IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public ActionResult Login([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var jwtAuthenticationManager = new JwtAuthenticationManager(_userService);
            var userSession = jwtAuthenticationManager.GenerateJwtToken(loginRequest.UserName, loginRequest.Password);

            if (userSession is null)
                return Unauthorized();
            else
                return Ok(userSession);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
        
    }
    
    
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("response");
    }
}