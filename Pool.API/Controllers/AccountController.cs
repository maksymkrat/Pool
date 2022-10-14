using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pool.API.Authentication;
using Pool.API.Services;
using Pool.Shared.Models;

namespace Pool.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private UserAccountService _userAccountService;

    public AccountController(UserAccountService userAccountService0)
    {
        _userAccountService = userAccountService0;
    }
    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public ActionResult<UserSession> Login([FromBody] LoginRequest loginRequest)
    {
        var jwtAuthenticationManager = new JwtAuthenticationManager(_userAccountService);
        var userSession = jwtAuthenticationManager.GenerateJwtToken(loginRequest.UserName, loginRequest.Password);

        if (userSession is null)
            return Unauthorized();
        else
            return userSession;
    }
    
    
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("response");
    }
}