using Fizzle.Managers;
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
        camera.ConstrainWorldBounds(Vector2.Zero, new(tileMapManager.currentMap.Map.Width * tileMapManager.currentMap.Map.TileWidth,
            tileMapManager.currentMap.Map.Height *
            tileMapManager.currentMap.Map.TileHeight));

        tileMapManager.Update(gameTime);
        transform = camera.ViewMatrix;

        var mainPlayer = players.players.FirstOrDefault(x => x.MainPlayer).Position;
        camera.LookingPosition = mainPlayer;


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
        ImGui.BeginListBox("Keybinds");

        foreach (var bind in players.players.First().controller.Binds)
            ImGui.Text($"{bind.Key} = {bind.Value}");
        ImGui.Text($"{players.players[0].controller.Direction}"); 
        ImGui.EndListBox();
        ImGui.EndMenu();
        ImGui.EndMenuBar();
    }


    #endregion

}