#pragma warning disable CS8604
#pragma warning disable CS8600
#pragma warning disable CS8602
#pragma warning disable CA2208
#pragma warning disable CA2249
#pragma warning disable CA1822
#pragma warning disable SYSLIB1045

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
// ReSharper disable HeapView.ClosureAllocation
// ReSharper disable TooWideLocalVariableScope
// ReSharper disable MemberCanBeMadeStatic.Global
// ReSharper disable ConvertToLocalFunction
// ReSharper disable MemberCanBeMadeStatic.Local

using System.Diagnostics.CodeAnalysis;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Calculator_REST.Models;
using Calculator_REST.Repository;
using Npgsql.Replication.TestDecoding;

namespace Calculator_REST.Services;

// "\\s*" + // >=0 spaces in the beginning of the string
// "[-+]?" + // not required + or - before first number
// "(\\d+(\\.\\d+)?([eE][-+]?\\d+)?)" + // number with float point or in exponent form
// "\\s*" + // >=0 spaces after first number
// "([+\\-*/])" + // operator
// "\\s*" + // >=0 spaces before second number
// "[-+]?" + // not required + or - before second number
// "(\\d+(\\.\\d+)?([eE][-+]?\\d+)?)" + // number with float point or in exponent form
// "\\s*" // >=0 spaces in the end of the string

public class CalculatorService
{
    
    private static readonly Regex BinaryOperationRegex = new(
        "\\s*[-+]?(\\d+(\\.\\d+)?([eE][-+]?\\d+)?)" +
        "\\s*([+\\-*/])" +
        "\\s*[-+]?(\\d+(\\.\\d+)?([eE][-+]?\\d+)?)" +
        "\\s*");

    private static readonly Regex OperatorsOnlyRegex = new(
        "[a-zA-Z0-9.,]");

    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    private readonly CalculatorDbContext _calculatorDbContext;

    public CalculatorService(CalculatorDbContext calculatorDbContext)
    {
        _calculatorDbContext = calculatorDbContext;
    }

    public void Test(Expression expression)
    {
        System.Data.DataTable dataTable = new();
        dataTable.Columns.Add("expression", string.Empty.GetType(), expression.InitialStringValue);
        System.Data.DataRow row = dataTable.NewRow();
        dataTable.Rows.Add(row);
        var result = double.Parse((string)row["expression"]);
        Console.WriteLine(result);
    }
    
    // private void CheckEmptyExpression(Expression expression)
    // {
    //     string expressionString = expression.InitialStringValue;
    //     if (string.IsNullOrEmpty(expressionString))
    //         throw new ArgumentNullException(nameof(expressionString));
    // }

    // public Expression CalculateExpression(Expression expression)
    // {
    //     string calculationResult;
    //     string calculatingString;
    //     int highestPriority;
    //     int nextHighestPriority;
    //     
    //     calculationResult = "";
    //     calculatingString = expression.InitialStringValue;
    //     CheckEmptyExpression(expression);
    //     
    //     expression = ParseExpression(expression);
    //     
    //     highestPriority = GetHighestOperatorPriority(expression.InitialStringValue);
    //     Func<Operation, bool> priorityFilter = operation => operation.Operator.Priority == highestPriority;
    //
    //     expression.ExpressionHistory.Add(expression.InitialStringValue);
    //     while (true)
    //     {
    //         highestPriority = GetHighestOperatorPriority(calculatingString);
    //         foreach (Operation @operation in expression.Operations.Where(priorityFilter))
    //         {
    //             calculationResult = CalculateOperation(@operation);
    //             expression.StringValue = calculatingString.Replace(
    //                 @operation.GetOperationString(), calculationResult);
    //             expression.ExpressionHistory.Add(calculatingString);
    //             expression.Operations.Remove(@operation);
    //         }
    //         nextHighestPriority = GetHighestOperatorPriority(calculatingString);
    //         if (highestPriority == nextHighestPriority)
    //             break;
    //     }
    //
    //     expression.Result = calculationResult;
    //     return expression;
    // }

    // private char GetOperatorFromOperation(string expressionSubString)
    // {
    //     var operationsDictionary = Operations.OperationsDictionary;
    //     char foundOperator;
    //
    //     foundOperator = operationsDictionary.Keys.FirstOrDefault(expressionSubString.Contains);
    //     return foundOperator != '\0' ? foundOperator : '\0';
    // }
    //
    // private List<string> GetOperandFromOperation(
    //     string expressionSubString, char operatorChar)
    // {
    //     string firstNumber, secondNumber;
    //     List<string> numbers = new();
    //     int operatorPos;
    //
    //     operatorPos = expressionSubString.IndexOf(operatorChar);
    //     firstNumber = expressionSubString[..operatorPos];
    //
    //     if ((operatorPos + 1) != (expressionSubString.Length - 1))
    //     {
    //         secondNumber = expressionSubString.Substring(
    //             operatorPos + 1,
    //             expressionSubString.Length - 1);
    //     }
    //     else
    //     {
    //         secondNumber = expressionSubString[^1].ToString();
    //     }
    //
    //     numbers.Add(firstNumber);
    //     numbers.Add(secondNumber);
    //     return numbers;
    // }
    //
    // private string CalculateOperation(Operation operation)
    // {
    //     string calculationResult;
    //     Dictionary<char, Func<double, double, double>> operationsDictionary;
    //
    //     operationsDictionary = Operations.OperationsDictionary;
    //     calculationResult = (operationsDictionary[operation.Operator.Value](
    //             Convert.ToDouble(operation.Operands[0], CultureInfo.InvariantCulture),
    //             Convert.ToDouble(operation.Operands[1], CultureInfo.InvariantCulture))
    //         .ToString(CultureInfo.InvariantCulture));
    //     return calculationResult;
    // }

    // private int GetHighestOperatorPriority(string expression)
    // {
    //     // TO DO: Rewrite with new Expression structure.
    //     List<int> resultPriorities = new();
    //     HashSet<char> operators;
    //     int highestPriority;
    //
    //     operators = Regex.Replace(expression, OperatorsOnlyRegex.ToString(), "").ToHashSet();
    //
    //     foreach (char @operator in operators)
    //         resultPriorities.Add(@operator);
    //
    //     highestPriority = resultPriorities.Max();
    //     return highestPriority;
    // }

    // private int GetOperatorPriority(char operatorChar)
    // {
    //     int priority;
    //     Dictionary<int, List<char>> priorities;
    //
    //     priorities = Operations.OperationsPrioritiesDictionary;
    //     priority = priorities.FirstOrDefault(x => x.Value.Contains(operatorChar)).Key;
    //
    //     return priority;
    // }

    // private Expression ParseExpression(Expression expression)
    // {
    //     List<string> operands;
    //     string match;
    //     string expressionString;
    //
    //     expressionString = expression.InitialStringValue;
    //
    //     while (!double.TryParse(s: expressionString, result: out _,
    //                provider: CultureInfo.InvariantCulture))
    //     {
    //         char operatorChar;
    //         Operator @operator;
    //         Operation operation;
    //         int operatorPriority;
    //
    //         match = Regex.Match(expressionString, BinaryOperationRegex.ToString()).ToString();
    //         operatorChar = GetOperatorFromOperation(match.ToString());
    //         operands = GetOperandFromOperation(match.ToString(), operatorChar);
    //         expressionString = expressionString.Replace(match.ToString(), operands[1]);
    //         operatorPriority = GetOperatorPriority(operatorChar);
    //
    //         @operator = new Operator(operatorChar, operatorPriority);
    //         operation = new Operation(@operator, operands);
    //         expression.Operations.Add(operation);
    //     }
    //
    //     return expression;
    // }
}