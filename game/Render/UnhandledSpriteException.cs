using System;

public class UnhandledSpriteException : Exception
{
    public UnhandledSpriteException()
    {
    }

    public UnhandledSpriteException(string message)
        : base(message)
    {
    }

    public UnhandledSpriteException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
