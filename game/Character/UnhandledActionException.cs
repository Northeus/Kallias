using System;

public class UnhandledActionException : Exception
{
    public UnhandledActionException()
    {
    }

    public UnhandledActionException(string message)
        : base(message)
    {
    }

    public UnhandledActionException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
