// ReSharper disable HeapView.ObjectAllocation.Evident

using System.Net.Sockets;

namespace Calculator_REST.Models;

public abstract class Operators
{
    public static readonly Dictionary<char, Func<double, double, double>> OperatorsDictionary = new()
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

    public static readonly Dictionary<int, List<char>> OperationsPriority = new()
    {
        { 0, new List<char> { '*', '/' } },
        { 1, new List<char> { '+', '-', '%', '^' } },
    };
}