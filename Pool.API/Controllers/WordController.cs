using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;
using Pool.Shared.Models.DeserializeTranslation;

namespace Pool.API.Controllers;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WordController : ControllerBase
{

    private readonly IWordService _wordService;
    private readonly ITranslatorService _translatorService;

    public WordController(IWordService service, ITranslatorService translatorService)
    {
        _wordService = service;
        _translatorService = translatorService;
    }

    private readonly Guid UserId = new Guid("23a2dcb7-38b5-44b4-85e9-9e6af7f4646f"); //need delete

    [HttpGet("GetAllUsersWords/{userId}")]
    public async Task<IActionResult> GetAllUsersWords(Guid userId)
    {
        try
        {
            var result = await _wordService.GetAllUsersWords(userId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
   
    [HttpGet("GetFourRandomWords/{userId}")]
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
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            var result = await _wordService.DeleteWordById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    [HttpGet("GetRandomWord/{userId}")]
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

    [HttpGet("Translate/{word}")]
    public async Task<IActionResult> Translate(string word)
    {
        try
        {
            var result = await _translatorService.Translate(word);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("AddWord")]
    public async Task<IActionResult> AddWord([FromBody] Word word)
    {
        try
        {
            var result = _wordService.AddWord(word);
            return Ok(result);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] Word word)
    {
        try
        {
            var result = _wordService.Update(word);
            return Ok(result);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

}