// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable JoinDeclarationAndInitializer

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Calculator_REST.Services;
using Calculator_REST.Models;

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
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation")]
    public ActionResult<Expression> CalculateExpression([FromBody] Expression expression)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"Expression: {expression.InitialStringValue}");
        Console.ResetColor();
        _calculatorService.Test(expression);
        // expression = _calculatorService.CalculateExpression(expression);
        return Ok(expression);
    }
}

// [ApiController]
// [Route("/")]
// [ApiExplorerSettings(IgnoreApi = true)]
// [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Evident")]
// public class SwaggerRedirectionControl : ControllerBase
// {
//     [HttpGet]
//     public async Task<RedirectResult> Redirect()
//     {
//         return await Task.Run(() =>
//             new RedirectResult("https://localhost:5432/swagger/index.html"));
//     }
// }