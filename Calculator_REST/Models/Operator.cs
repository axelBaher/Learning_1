namespace Calculator_REST.Models;

public class Operator
{
    public char Value { get; set; }
    public int Priority { get; set; }

    public Operator()
    {
    }

    public Operator(char value, int priority)
    {
        Value = value;
        Priority = priority;
    }
}