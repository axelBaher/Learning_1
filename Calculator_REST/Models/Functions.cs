using System.Diagnostics.CodeAnalysis;

namespace Calculator_REST.Models;

public static class Functions
{
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Evident")]
    public static Dictionary<string, Func<double, double>> FunctionsDictionary { get; } = new()
    {
        { "sin", Math.Sin },
        { "sinh", Math.Sinh },
        { "Asin", Math.Asin },
        { "Asinh", Math.Asinh },
        { "cos", Math.Cos },
        { "cosh", Math.Cosh },
        { "Acos", Math.Acos },
        { "Acosh", Math.Acosh },
        { "tan", Math.Tan },
        { "tanh", Math.Tanh },
        { "Atan", Math.Atan },
        { "Atanh", Math.Atanh },
        { "ctg", x => 1.0 / Math.Tan(x) },
        { "ctgh", x => 1.0 / Math.Tanh(x) },
        { "Actg", x => Math.PI / 2 - Math.Atan(x) },
        { "Actgh", x => 0.5 * (Math.Log(1 + x) - Math.Log(1 - x)) },
        { "logE", Math.Log},
        { "log10", Math.Log10},
        // { "|", x => Functions.GetAbsString(x.ToString())},
    };
    // [SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
    // public static double GetAbsString(string expression)
    // {
    //     foreach (char c in expression)
    //     {
    //         Console.WriteLine(c);
    //     }
    //
    //     return 0.0;
    //     // return double.Parse(expression);
    //     // return ((IConvertible)expression).ToDouble(null);
    // }
}