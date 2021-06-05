using System;

namespace game.Render
{
    /// <summary>
    /// Exception fo unhandled sprite inside renderer.
    /// </summary>
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
}
