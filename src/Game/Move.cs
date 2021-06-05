using Discord;

namespace Kallias.Game
{
    /// <summary>
    /// Represent one move for game with corresponding emoji fo GUI.
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Create new move from emoji and Id specifier, which might be useful
        /// for easier handling of moves.
        /// </summary>
        /// <param name="emote"></param>
        /// <param name="id"></param>
        public Move(string emote, int id)
            => (Emote, Id) = (new Emoji(emote), id);

        /// <summary>
        /// Emote representing given move.
        /// </summary>
        public IEmote Emote { get; } 

        /// <summary>
        /// Id specifier of given move.
        /// </summary>
        public int Id { get; }
    }
}