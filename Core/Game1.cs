using Fizzle.Managers;
using MonoGame.ImGuiNet;
namespace Fizzle.Core;

public class Game1 : Game
{
    internal static GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    private readonly GameStateManager gsm;
    
    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        gsm = new GameStateManager();
    }

    protected override void Initialize()
    {
        Window.Title = Data.Window.Title;
        Window.AllowAltF4 = true;

        graphics.PreferredBackBufferWidth = Data.Window.ScreenW;
        graphics.PreferredBackBufferHeight = Data.Window.ScreenH;
        graphics.ApplyChanges();


        Data.Game.Content = Content;
        Data.GUI.Renderer = new ImGuiRenderer(this);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Data.GUI.Renderer.RebuildFontAtlas();

        spriteBatch = new SpriteBatch(GraphicsDevice);
        gsm.LoadContent(Content);

    }

    protected override void Update(GameTime gameTime)
    {
        checkAppRegainedFocus();

        Data.Game.GameTime(gameTime);

        if (!IsActive)
            return;

        gsm.Update(gameTime);
        if (Data.Window.Exit)
            Exit();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        Data.GUI.Renderer.BeginLayout(gameTime);
        gsm.Draw(spriteBatch);
        Data.GUI.Renderer.EndLayout();

        base.Draw(gameTime);
    }

    private bool wasFocused;
    private void checkAppRegainedFocus()
    {
        if (!IsActive)
            wasFocused = false;
        else if (!wasFocused && graphics.IsFullScreen)
        {
            wasFocused = true;
            graphics.ToggleFullScreen();
            graphics.ToggleFullScreen();
        }
    }
}
