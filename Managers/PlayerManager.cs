using Fizzle.Models;
using System.Diagnostics;

namespace Fizzle.Managers
{
    internal class PlayerManager : IFizzleComponent
    {

        public readonly List<Player> players;

        public PlayerManager()
        {
            players = new List<Player>()
            {
                new Player("sprites/player.sf", 1.5f, new Vector2(400, 400), true,ControlSchemes.WASD),
                new Player("sprites/player.sf", 1.5f, new Vector2(500, 500), true, ControlSchemes.ARROW_KEYS),
            };
        }

        public void LoadContent(ContentManager Content)
        {
            foreach (var player in players)
                player.LoadContent(Content);


        }

        public void Update(GameTime gameTime)
        {
            players.ForEach(player =>
            {
                player.Update(gameTime);


            });


            CheckAginstPlayerCollision(players[0], players[1].Hitbox);
            CheckAginstPlayerCollision(players[1], players[0].Hitbox);


        }

        public void CheckAginstPlayerCollision(in Player player, Rectangle target)
        {
            if (player.Velocity.X > 0 && player.Collider.IsTouchingLeft(player.Hitbox, player.Velocity, target) || player.Collider.IsTouchingRight(player.Hitbox, player.Velocity, target))
            {
                player.Velocity.X = 0f;

            }

            if (player.Collider.IsTouchingTop(player.Hitbox, player.Velocity, target) || player.Collider.IsTouchingBottom(player.Hitbox, player.Velocity, target))
            {
                player.Velocity.Y = 0f;

            }
        }

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
