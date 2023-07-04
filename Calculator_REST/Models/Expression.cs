namespace Calculator_REST.Models;

public class Expression
{
    public string? ExpressionString { get; }
    public Expression() {}
    public Expression(string? expression)
    {
        this.ExpressionString = expression;
    }
    
}