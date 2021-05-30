using Discord;

namespace Kallias.Game
{
    public class Move
    {
        public Move(string emote, int id)
            => (Emote, Id) = (new Emoji(emote), id);

        public IEmote Emote { get; } 

        public int Id { get; }
    }
}