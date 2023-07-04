namespace Calculator_REST.Models;

public class Operations
{
    public Dictionary<char, string> OperationDictionary = new()
    {
        { '+', "add" },
        { '-', "sub" },
        { '*', "mult" },
        { '/', "divide" },
        { '%', "rem" },
        { '^', "pow" },
        { '@', "abs" },
        { '(', "open_bracket" },
        { ')', "close_bracket" },
    };
}