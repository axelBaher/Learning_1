#pragma warning disable CS8604
#pragma warning disable CS8600
#pragma warning disable CS8602
#pragma warning disable CA2208
#pragma warning disable CA2249

// ReSharper disable HeapView.ObjectAllocation.Possible
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable HeapView.DelegateAllocation
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable HeapView.ObjectAllocation
// ReSharper disable HeapView.ObjectAllocation.Evident
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
// ReSharper disable ReturnTypeCanBeEnumerable.Local

// using Calculator_REST.Repository;
using System.Globalization;
using Calculator_REST.Models;
using System.Text.RegularExpressions;

namespace Calculator_REST.Services;

public partial class CalculatorService
{
    [GeneratedRegex(
        "" +
        "\\s*" +                               // >=0 spaces in the beginning of the string
        "[-+]?" +                              // not required + or - before first number
        "(\\d+(\\.\\d+)?([eE][-+]?\\d+)?)" +   // number with float point or in exponent form
        "\\s*" +                               // >=0 spaces after first number
        "([+\\-*/])" +                         // operator
        "\\s*" +                               // >=0 spaces before second number
        "[-+]?" +                              // not required + or - before second number
        "(\\d+(\\.\\d+)?([eE][-+]?\\d+)?)" +   // number with float point or in exponent form
        "\\s*"                                 // >=0 spaces in the end of the string
    )]
    private static partial Regex MyRegex();

    // private readonly CalculatorDbContext _calculatorDbContext;
    //
    // public CalculatorService(CalculatorDbContext calculatorDbContext)
    // {
    //     _calculatorDbContext = calculatorDbContext;
    // }
    public static void CheckEmptyExpression(Expression expression)
    {
        string expressionString = expression.ExpressionString;
        if (string.IsNullOrEmpty(expressionString))
            throw new ArgumentNullException(nameof(expressionString));
    }

    public static Expression CalculateExpression(Expression expression)
    {
        Expression newExpression = new();
        string expressionString = expression.ExpressionString;
        while (!double.TryParse(s:expressionString, result:out double resultNumber, provider:CultureInfo.InvariantCulture))
        {
            string match = MyRegex().Match(expressionString).ToString();
            Dictionary<char, Func<double, double, double>> operators = Operators.OperatorsDictionary;
            char operatorChar = GetOperatorFromString(match, operators);
            List<string> numbers = GetNumbersFromString(match, operatorChar);
            string result = OperationExecute(operators, operatorChar, numbers);
            string newExpressionString = ExpressionReplace(expressionString, match, result);
            expressionString = newExpressionString;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Expression: {expressionString}");
            Console.ResetColor();
        }
        
        newExpression.ExpressionString = expressionString;
        return newExpression;
    }

    private static char GetOperatorFromString(
        string expressionSubString,
        Dictionary<char, Func<double, double, double>> operations)
    {
        char foundOperator = operations.Keys.FirstOrDefault(expressionSubString.Contains);
        return foundOperator != '\0' ? foundOperator : '\0';
    }

    private static List<string> GetNumbersFromString(
        string expressionSubString, char operatorChar)
    {
        string firstNumber, secondNumber;
        List<string> numbers = new();
        int operatorPos = expressionSubString.IndexOf(operatorChar);
        firstNumber = expressionSubString[..operatorPos];
        if ((operatorPos + 1) != (expressionSubString.Length - 1))
            secondNumber = expressionSubString.Substring(
                operatorPos + 1,
                expressionSubString.Length - 1);
        else
            secondNumber = expressionSubString[^1].ToString();
        numbers.Add(firstNumber);
        numbers.Add(secondNumber);
        return numbers;
    }

    private static string OperationExecute(
        IReadOnlyDictionary<char, Func<double, double, double>> operators,
        char operatorChar, IReadOnlyList<string> numbers)
    {
        double result = operators[operatorChar](
            Convert.ToDouble(numbers[0], CultureInfo.InvariantCulture),
            Convert.ToDouble(numbers[1], CultureInfo.InvariantCulture));
        return Convert.ToString(result, CultureInfo.InvariantCulture);
    }

    private static string ExpressionReplace(
        string expressionString, string match, string result)
    {
        expressionString = expressionString.Replace(match, result);
        return expressionString;
    }
}