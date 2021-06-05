using System;

namespace game.Character
{
    /// <summary>
    /// Create new exception for unhandled action.
    /// </summary>
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

}
