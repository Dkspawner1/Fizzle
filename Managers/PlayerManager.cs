using Fizzle.Models;

namespace Fizzle.Managers
{
    internal class PlayerManager : IFizzleComponent
    {
        internal List<Sprite> sprites = new List<Sprite>();


        public void LoadContent(ContentManager Content)
        {
            sprites.Add(new Sprite($"sprites/player.sf", 1.5f, new(200, 200), true));


            foreach (var sprite in sprites)
                sprite.LoadContent(Content);

        }

        public void Update(GameTime gameTime)
        {
            foreach (var sprite in sprites)
                sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);
        }

    }
}
