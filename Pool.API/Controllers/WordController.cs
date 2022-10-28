using Microsoft.AspNetCore.Mvc;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;

namespace Pool.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WordController : ControllerBase
{

    private readonly IWordService _wordService;

    public WordController(IWordService service)
    {
        _wordService = service;
    }


    [HttpGet("GetAllUsersWords")]
    public async Task<IActionResult> GetAllUsersWords()
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

}