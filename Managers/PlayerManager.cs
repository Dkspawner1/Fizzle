using Fizzle.Models;
using MonoGame.Extended.Sprites;

namespace Fizzle.Managers
{
    internal class PlayerManager : IFizzleComponent
    {

        public readonly List<Player<PlayerController>> players;


        public PlayerManager()
        {
            players = new List<Player<PlayerController>>();
        }

        public void LoadContent(ContentManager Content)
        {
            players.Add(new Player<PlayerController>("sprites/player.sf", 1.5f, new Vector2(400,400) ,true));

            foreach (var player in players)
            {
                player.LoadContent(Content);
            }
        }

        public void Update(GameTime gameTime)
        {


            foreach (var player in players)
            {
                player.Update(gameTime);
                float walkSpeed = Data.Game.TotalSeconds * 128;
                player.Move(in walkSpeed);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var player in players)
                player.Draw(spriteBatch);
        }

    }
}
