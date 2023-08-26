using Microsoft.AspNetCore.Mvc;
using Pool.API.Services.IServicec;

namespace Pool.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IrregularVerbController : ControllerBase
{
    
    private readonly IIrregularVerbService _irregularVerbService;

    public IrregularVerbController(IIrregularVerbService irregularVerbService)
    {
        _irregularVerbService = irregularVerbService;
        
    }


    [HttpGet("GetRandomIrregularVerb")]
    public async Task<IActionResult> GetRandomIrregularVerb()
    {
        try
        {
            var result = await _irregularVerbService.GetRandomIrregularVerb();
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}