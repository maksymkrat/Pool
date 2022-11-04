using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pool.API.Services.IServicec;

namespace Pool.API.Controllers;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WordController : ControllerBase
{

    private readonly IWordService _wordService;

    public WordController(IWordService service)
    {
        _wordService = service;
    }

    private readonly Guid UserId = new Guid("23a2dcb7-38b5-44b4-85e9-9e6af7f4646f"); //need delete

    [HttpGet("GetAllUsersWords/{userId}")]
    public async Task<IActionResult> GetAllUsersWords(Guid userId)
    {
        try
        {
            var result = await _wordService.GetAllUsersWords();
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetRandomWord/{userId}")]
    //[HttpGet("GetRandomWord")]
    public async Task<IActionResult> GetRandomWord(Guid userId)
    {
        try
        {
            var result = await _wordService.GetRandomWord(userId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    [HttpGet("GetFourRandomWords/{userId}")]
    //[HttpGet("GetFourRandomWords")]
    public async Task<IActionResult> GetFourRandomWords(Guid userId)
    {
        
        try
        {
            var result = await _wordService.GetFourRandomWords(userId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

}