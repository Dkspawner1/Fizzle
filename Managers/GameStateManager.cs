using Fizzle.Core;
using Fizzle.Scenes;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;
using System.Threading;
namespace Fizzle.Managers;

public class GameStateManager : IFizzleComponent
{
    private readonly MenuScene ms;
    private readonly GameScene gs;

    // Loading stuff
    bool isLoaded;
    SpriteFont loadingFont;
    Texture2D loadingImage;
    //

    public GameStateManager()
    {
        ms = new MenuScene();
        gs = new GameScene();

    }
    public void LoadContent(ContentManager Content)
    {
        
        loadingFont = Content.Load<SpriteFont>("Fonts/LoadingScreenFont");
        loadingImage = Content.Load<Texture2D>("textures/btn0");
        ms.LoadContent(Content);
        gs.LoadContent(Content);

    }

    public void Update(GameTime gameTime)
    {

        InputManager.UpdateMouse();
        InputManager.UpdateKeyboard();

        if (InputManager.kb.IsKeyDown(Keys.L))
            isLoaded = true;

        if (isLoaded)
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
    }

    public void Draw(SpriteBatch spriteBatch)
    {

        if (!isLoaded)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(loadingImage, new Rectangle(0, 0, 500, 500), Color.White);
            spriteBatch.DrawString(loadingFont, "LOADING...", new Vector2(Data.Window.ScreenW / 2 - loadingFont.MeasureString("LOADING...").X / 2,
                Data.Window.ScreenH / 2), Color.Black, 0, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0);
            spriteBatch.End();
        }
        if (isLoaded)
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

}
