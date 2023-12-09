using Fizzle.Models;
using Fizzle.Tile;

namespace Fizzle.Managers
{
    internal class PlayerManager : IFizzleComponent
    {
        public readonly List<Player> players;
        public PlayerManager() => players = new List<Player>()
            {
                new("sprites/player.sf", 1.45f, new Vector2(400, 400),
                    true,ControlSchemes.WASD),
                new("sprites/dorll.sf", 1.45f, new Vector2(500, 500),
                    false, ControlSchemes.ARROW_KEYS),
            };

        public void LoadContent(ContentManager Content)
        {
            foreach (var player in players)
                player.LoadContent(Content);
        }

        public void Update(GameTime gameTime) => players.ForEach(player =>
        {
            player.Update(gameTime);

            player.Collider.CheckPlayerCollision(player, players[0].Hitbox);
            player.Collider.CheckPlayerCollision(player, players[1].Hitbox);
        });


        public void Draw(SpriteBatch spriteBatch)
        {
            players.ForEach(player =>
            {
                player.Draw(spriteBatch);
                player.Controller.DrawUI();
            });

        }
    }
}
