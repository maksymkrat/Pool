using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pool.API.Authentication;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;

namespace Pool.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger _logger;

    public AccountController( IUserService userService, ILogger<AccountController> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequestModel loginRequest)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: Login");
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

    [HttpPost]
    [Route("Create")]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] RegistrationModel newUser)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: Create");

        try
        {
            var result = await _userService.CreateUser(newUser);
            return Ok(result);
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