using System.Linq;
using System.Collections.Generic;
using Kallias.Data;
using Kallias.Game.Graphic;
using game.Character;
using game.Environment;
using game.Extensions;

namespace game.Render
{
    public static class PlayerRenderer
    {
        public static Canvas GetCanvas(Player player, World world)
        {
            var playerLayer = new string[world.Size.Height, world.Size.Width].Map(
                (string _) => " "
            );

            playerLayer[player.Position.Y, player.Position.X] = DatabaseEmotes<Sprite>.Database[Sprite.Robot];

            return new Canvas(playerLayer.GetAllRows());
        }
    }
}