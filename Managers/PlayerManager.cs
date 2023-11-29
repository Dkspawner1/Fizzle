using Fizzle.Models;

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
            players.Add(new Player<PlayerController>("sprites/player.sf", 1.5f, new Vector2(400, 400), false));
            players.Add(new Player<PlayerController>("sprites/player.sf", 1.5f, new Vector2(500, 500), true));

            players[0].controller.LoadKeybinds(PlayerController.ControlScheme.WASD);
            players[1].controller.LoadKeybinds(PlayerController.ControlScheme.ARROW_KEYS);

            foreach (var player in players)
                player.LoadContent(Content);
        }

        public void Update(GameTime gameTime)
        {
            float walkSpeed = Data.Game.TotalSeconds * 128;
            players.ForEach(player => 
            {
                player.Update(gameTime);
                player.Move(in walkSpeed);
            });
        }

        public void Draw(SpriteBatch spriteBatch) => players.ForEach(player => player.Draw(spriteBatch));

    }
}
