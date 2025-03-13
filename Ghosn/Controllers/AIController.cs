// Ghosn/Controllers/AIController.cs
using Microsoft.AspNetCore.Mvc;
using Ghosn_BLL.Services;

namespace Ghosn.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly AIService _aiService;

    public AIController(AIService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("completion")]
    public async Task<IActionResult> GetCompletion([FromBody] PromptRequest request)
    {
        if (string.IsNullOrEmpty(request.Prompt))
        {
            return BadRequest("Prompt cannot be empty");
        }

        var result = await _aiService.GetAICompletionAsync(request.Prompt);

        if (result.Error)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }
}

public class PromptRequest
{
    public string Prompt { get; set; } = string.Empty;
}