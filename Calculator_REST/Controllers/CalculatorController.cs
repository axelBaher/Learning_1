using Calculator_REST.Services;
using Calculator_REST.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calculator_REST.Controllers;

[ApiController]
[Route("controller")]
[Produces("application/json")]
public class CalculatorController : ControllerBase
{
    private readonly CalculatorService _calculatorService;

    public CalculatorController(CalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
    }

    [HttpPost("calculate")]
    public IActionResult GetExpression([FromBody] Expression expression)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"Expression: {expression.ExpressionString}");
        Console.ResetColor();
        return Ok();
    }
}

[ApiController]
[Route("/")]
[ApiExplorerSettings(IgnoreApi = true)]
public class SwaggerRedirectionControl : ControllerBase
{
    [HttpGet]
    public async Task<RedirectResult> Redirect()
    {
        return await Task.Run(() =>
            new RedirectResult("https://localhost:5432/swagger/index.html"));
    }
}