using Fizzle.Managers;
using Fizzle.Tile;
using ImGuiNET;
using MLEM.Cameras;

namespace Fizzle.Scenes;

public class GameScene : IFizzleComponent
{
    private readonly TileMapManager tileMapManager;
    private Camera camera;
    private Matrix transform;

    public GameScene()
    {
        tileMapManager = new();
    }
    public void LoadContent(ContentManager Content)
    {
        tileMapManager.LoadContent(Content);
        camera = new Camera(Game1.graphics.GraphicsDevice, true)
        {
            AutoScaleWithScreen = true,
             Scale = 2f
        };
        scale = camera.Scale;
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


        DrawDebugGUI(ref scale);
    }
    private float scale;

    private void DrawDebugGUI(ref float scale)
    {
        ImGui.BeginMenuBar();
        camera.Scale = scale;
        ImGui.BeginMenu("Camera", true);
        ImGui.SliderFloat("Zoom", ref scale, 1f, 5f, default, ImGuiSliderFlags.NoRoundToFormat);



        ImGui.EndMenu();
        ImGui.EndMenuBar();
    }




}