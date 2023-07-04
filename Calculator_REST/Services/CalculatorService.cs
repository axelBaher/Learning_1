using Calculator_REST.Repository;
using Calculator_REST.Models;
namespace Calculator_REST.Services;

public class CalculatorService
{
    private readonly CalculatorDbContext _calculatorDbContext;

    public CalculatorService(CalculatorDbContext calculatorDbContext)
    {
        _calculatorDbContext = calculatorDbContext;
    }

    public Expression ParseExpression(Expression expression)
    {
        
        return expression;
    }
}