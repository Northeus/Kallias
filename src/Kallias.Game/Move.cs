using Discord;

namespace Kallias.Game
{
    public class Move
    {
        public Move(string emote)
            => Emote = new Emoji(emote);

        public IEmote Emote { get; } 
    }
}