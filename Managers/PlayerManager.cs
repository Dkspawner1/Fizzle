using Fizzle.Models;
using System.Security.Cryptography.X509Certificates;

namespace Fizzle.Managers
{
    internal class PlayerManager : IFizzleComponent
    {
        public readonly List<Player> players;
        public PlayerManager() => players = new List<Player>()
            {
                new("sprites/player.sf", 1.45f, new Vector2(400, 400),
                    true,ControlSchemes.WASD),
                new("sprites/player.sf", 1.45f, new Vector2(500, 500),
                    false, ControlSchemes.ARROW_KEYS),
            };

        public void LoadContent(ContentManager Content)
        => players.ForEach(player => player.LoadContent(Content));

        public void Update(GameTime gameTime) => players.ForEach(player =>
        {
            player.Collider.CheckAginstPlayerCollision(player, players[0].Hitbox);
            player.Collider.CheckAginstPlayerCollision(player, players[1].Hitbox);
            player.Update(gameTime);


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
