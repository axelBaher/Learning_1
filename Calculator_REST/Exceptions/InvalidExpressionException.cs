using System;

namespace Calculator_REST.Exceptions;

public class InvalidExpressionException : Exception
{
    public InvalidExpressionException(string message)
        : base(message) { }
}