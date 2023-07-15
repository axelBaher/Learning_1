#pragma warning disable CS8601
#pragma warning disable CS8618

namespace Calculator_REST.Models;

public class Operation
{
    public Operator Operator { get; set; }
    public List<string> Operands { get; set; }

    public Operation()
    {
    }

    public Operation(Operator @operator, List<string>? operands)
    {
        Operator = @operator;
        Operands = operands;
    }

    public string GetOperationString()
    {
        string operationString;
        operationString = Operands[0] + Operator.Value + Operands[1];
        return operationString;
    }
}