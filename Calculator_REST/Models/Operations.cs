// ReSharper disable HeapView.ObjectAllocation.Evident

namespace Calculator_REST.Models;

public abstract class Operations
{
    public static readonly Dictionary<char, Func<double, double, double>> OperationsDictionary = new()
    {
        { '+', (x, y) => x + y },
        { '-', (x, y) => x - y },
        { '*', (x, y) => x * y },
        { '/', (x, y) => x / y },
        { '%', (x, y) => x % y },
        { '^', Math.Pow },
        // { '@', "abs" },
        // { '(', "open_bracket" },
        // { ')', "close_bracket" },
    };

    // public static readonly Dictionary<int, List<char>> OperationsPrioritiesDictionary = new()
    // {
    //     { 0, new List<char> { '+', '-', '%', '^' } },
    //     { 1, new List<char> { '*', '/' } },
    // };
}