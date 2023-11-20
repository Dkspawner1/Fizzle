
using Fizzle.Scenes;

namespace Fizzle.Managers;

public class GameStateManager : IFizzleComponent
{
    private readonly MenuScene ms;
    private readonly GameScene gs;
    public GameStateManager()
    {
        ms = new MenuScene();
        gs = new GameScene();
    }
    public void LoadContent(ContentManager Content)
    {
        ms.LoadContent(Content);
        gs.LoadContent(Content);
    }

    public void Update(GameTime gameTime)
    {
        switch (Data.Game.CurrentState)
        {
            case Data.Game.GameStates.Menu:
                ms.Update(gameTime);
                break;
            case Data.Game.GameStates.Game:
                gs.Update(gameTime);
                break;
            case Data.Game.GameStates.Settings:
                break;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        switch (Data.Game.CurrentState)
        {
            case Data.Game.GameStates.Menu:
                ms.Draw(spriteBatch);
                break;
            case Data.Game.GameStates.Game:
                gs.Draw(spriteBatch);
                break;
            case Data.Game.GameStates.Settings:
                break;
        }
    }
}
