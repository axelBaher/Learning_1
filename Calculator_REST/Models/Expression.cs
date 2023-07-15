#pragma warning disable CS8618

namespace Calculator_REST.Models;

public class Expression
{
    public string InitialStringValue { get; set; }
    public string StringValue { get; set; }
    public List<Operation> Operations { get; set; }
    public List<string> ExpressionHistory { get; set; }
    public string Result { get; set; }
    
    public Expression()
    {
        Operations = new List<Operation>();
        ExpressionHistory = new List<string>();
        Result = "";
        StringValue = "";
    }

    public Expression(string initialStringValue, string stringValue, List<Operation> operations, List<string> expressionHistory, string result)
    {
        InitialStringValue = initialStringValue;
        StringValue = stringValue;
        Operations = operations;
        ExpressionHistory = expressionHistory;
        Result = result;
    }
}