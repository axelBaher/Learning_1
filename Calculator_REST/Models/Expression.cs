namespace Calculator_REST.Models;

public class Expression
{
    public string? ExpressionString { get; set; }

    public Expression()
    {
        // Console.ForegroundColor = ConsoleColor.DarkRed;
        // Console.WriteLine("Expression object created!");
        // Console.ResetColor();
    }
    public Expression(string? expression)
    {
        this.ExpressionString = expression;
    }
    
}