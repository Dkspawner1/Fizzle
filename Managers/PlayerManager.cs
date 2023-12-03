﻿using Fizzle.Models;

namespace Fizzle.Managers
{
    internal class PlayerManager : IFizzleComponent
    {
        public readonly List<Player> players;
        public PlayerManager() => players = new List<Player>()
            {
                new("sprites/player.sf", 1.45f, new Vector2(400, 400), true,ControlSchemes.WASD),
                new("sprites/player.sf", 1.45f, new Vector2(500, 500), true, ControlSchemes.ARROW_KEYS),
            };

        public void LoadContent(ContentManager Content)
        {
            foreach (var player in players)
                player.LoadContent(Content);
        }

        public void Update(GameTime gameTime) => players.ForEach(player =>
        {
            player.Update(gameTime);

            player.Collider.CheckAginstPlayerCollision(player, players[1].Hitbox);
            player.Collider.CheckAginstPlayerCollision(player, players[0].Hitbox);
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
