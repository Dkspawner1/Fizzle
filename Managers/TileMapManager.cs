using Fizzle.Managers;
using System.Linq;

namespace Fizzle.Tile;

// This class contains one tilemap and get's managed by: TileMapManager.cs  
public class TileMapManager : IFizzleComponent
{
    private readonly Dictionary<string, FizzleTileMap> tileMaps;
    private List<string> keyList;
    private Rectangle? DebugRect;

    private int currentMapIndex;
    private GUIManager gui;
    public TileMapManager()
    {
        tileMaps = new();
        keyList = new();
        currentMapIndex = 1;

        gui = new();
    }

    private FizzleTileMap currentMap;

    internal FizzleTileMap CurrentMap
    {
        get => currentMap;
        private set
        {
            currentMap.UnloadContent();
            currentMap = value;
            currentMap.LoadContent(Data.Game.Content);
        }
    }

    public void LoadContent(ContentManager Content)
    {
        tileMaps.Add("HubMap", new FizzleTileMap(FizzleTileMap.MapType.Hub));
        //tileMaps.Add("world1", new FizzleTileMap(FizzleTileMap.MapType.NONE));


        keyList.AddRange(tileMaps.Keys);
        for (int i = 0; i < tileMaps.Count; i++)
            tileMaps[keyList[i]].MapName = keyList[i];


        // Load the first map for the first time to setup my onchange property
        currentMap = tileMaps.Values.First();
        currentMap.LoadContent(Content);
    }

    public void Update(GameTime gameTime)
    {

        //if (InputManager.oldKb.IsKeyUp(Keys.D1) && InputManager.kb.IsKeyDown(Keys.D1))
        //{
        //    currentMap = tileMaps["world1"];
        //    currentMapIndex = 1;
        //}
        //if (InputManager.oldKb.IsKeyUp(Keys.D2) && InputManager.kb.IsKeyDown(Keys.D2))
        //{
        //    currentMap = tileMaps["world2"];
        //    currentMapIndex = 2;
        //}

        currentMap.Update(gameTime);

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currentMap.Draw(spriteBatch);
        currentMap.DrawRectangleColliders(spriteBatch);

        if (DebugRect is not null)
        {
            Texture2D texture = new Texture2D(Game1.graphics.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { new Color(Color.DarkRed, 1f) });
            spriteBatch.Draw(texture, (Rectangle)DebugRect, Color.White);
        }

        gui.DrawGUI("TileManager Runtime Variables", "Variables", true, tileMaps[keyList[currentMapIndex - 1]].MapName);


    }
}