using Fizzle.Managers;
using Fizzle.Tile;
using MLEM.Cameras;

namespace Fizzle.Scenes;

public class GameScene : IFizzleComponent
{
    private readonly TileMapManager tileMapManager;
    private Camera camera;
    private Matrix transform;

    private GUIManager gui;
    public GameScene()
    {
        tileMapManager = new();
        gui = new();
    }
    public void LoadContent(ContentManager Content)
    {
        tileMapManager.LoadContent(Content);
        camera = new Camera(Game1.graphics.GraphicsDevice, true)
        {
            AutoScaleWithScreen = true,
            Scale = 3.25f
        };
    }

    public void Update(GameTime gameTime)
    {
        tileMapManager.Update(gameTime);
        transform = camera.ViewMatrix;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: transform);
        tileMapManager.Draw(spriteBatch);
        spriteBatch.End();

    }




}