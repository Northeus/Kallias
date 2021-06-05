using Kallias.Data;
using Kallias.Game.Graphic;
using game.Character;
using game.Environment;
using game.Extensions;

namespace game.Render
{
    /// <summary>
    /// Produce canvas for given player.
    /// </summary>
    public static class PlayerRenderer
    {
        /// <summary>
        /// Create empty canvas with only player on specific position.
        /// </summary>
        /// <param name="player">Player to be placed on canvas.</param>
        /// <param name="world">World specifying canvas size.</param>
        /// <returns>Canvas just with player.</returns>
        public static Canvas GetCanvas(Player player, World world)
        {
            var playerLayer = new string[world.Size.Height, world.Size.Width].Map(
                _ => " "
            );

            playerLayer[player.Position.Y, player.Position.X] = DatabaseEmotes<Sprite>.Database[Sprite.Robot];

            return new Canvas(playerLayer.GetAllRows());
        }
    }
}