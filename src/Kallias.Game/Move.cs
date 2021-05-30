using Discord;

namespace Kallias.Game
{
    public class Move
    {
        public Move(string emote, int id)
            => Emote = new Emoji(emote);

        public IEmote Emote { get; } 

        public int Id { get; }
    }
}