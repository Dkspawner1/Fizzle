namespace Fizzle.Core;

public interface IFizzleComponent
{
    public void LoadContent(ContentManager Content);
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch);
}