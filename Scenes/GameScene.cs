using Fizzle.Managers;
using Fizzle.Models;
using Fizzle.Tile;
using ImGuiNET;
using MLEM.Cameras;
using System.Linq;

namespace Fizzle.Scenes;

public class GameScene : IFizzleComponent
{
    private readonly TileMapManager tileMapManager;
    private Camera camera;
    private Matrix transform;

    private PlayerManager players;

    public GameScene()
    {
        tileMapManager = new();
        players = new();
    }
    public void LoadContent(ContentManager Content)
    {
        tileMapManager.LoadContent(Content);
        camera = new Camera(Game1.graphics.GraphicsDevice, true)
        {
            AutoScaleReferenceSize = new Point(Data.Window.ScreenW, Data.Window.ScreenH),
            AutoScaleWithScreen = true,
            Scale = 2f,

        };
        scale = camera.Scale;

        players.LoadContent(Content);
    }

    public void Update(GameTime gameTime)
    {
        InputManager.UpdateKeyboard();

        camera.LookingPosition = players.sprites.FirstOrDefault(p => p.MainSprite).Position;    

        tileMapManager.Update(gameTime);
        transform = camera.ViewMatrix;

        players.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: transform);
        tileMapManager.Draw(spriteBatch);


        players.Draw(spriteBatch);
        spriteBatch.End();

        DrawDebugGUI(ref scale);
    }
    #region IMGUI 


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


    #endregion

}