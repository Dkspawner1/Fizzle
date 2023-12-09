using System.Linq;
using System;
using TiledCS;
using System.Diagnostics;
using MonoGame.Extended;
using System.Diagnostics.CodeAnalysis;

namespace Fizzle.Tile;

/// <summary>
/// This class is my own wrapper for TiledCS 
/// This class is a wrapper
/// This class takes in one map and contains all the elements.
/// this class gets managed by TileMapManager.cs
/// </summary>
public class FizzleTileMap : IFizzleComponent
{
    // Represents all of the tilesets in a map 
    public TiledMap Map { get; private set; }
    // The tileset images used in Tiled
    public Dictionary<int, TiledTileset> Tilesets { get; private set; }
    public int Id { get; }

    public enum MapType { Hub, OverWorld, Dungeon, NONE }
    public string MapName { get; set; }
    public string FolderName { get; set; }




    public readonly List<Texture2D> TilesetTextures;

    public TiledLayer CollisionLayer;
    public List<Rectangle> CollisionRectangles;


    //TODO: Change Fizzle Tilemap manager to take in this class as an interface and get rid of currentmap variable 
    [Flags]
    enum Translation
    {
        None = 0,
        Flip_H = 1 << 0,
        Flip_V = 1 << 1,
        Flip_D = 1 << 2,

        Rotate_90 = Flip_D | Flip_H,
        Rotate_180 = Flip_H | Flip_V,
        Rotate_270 = Flip_V | Flip_D,

        Rotate_90AndFlip_H = Flip_H | Flip_V | Flip_D,
    }


    public FizzleTileMap(MapType type)
    {
        TilesetTextures = new();
        CollisionLayer = new();
        CollisionRectangles = new();

        if (type == MapType.NONE) FolderName = string.Empty;
        else
            FolderName = new string(Enum.GetName(type) + "/");
    }

    public void LoadContent(ContentManager Content)
    {
        LoadMap(Content);

        // Every Tiled map should only have one collider layer
        CollisionLayer = Map.Layers.First(l => l.name == "collision".ToUpper());

        // Might not be working, check when collision is added into tiled map
        //TODO: Fix, new map collision is undetectable with current settings need to find a way to make this work
        foreach (var rectangle in CollisionLayer.objects)
        {

            //Trace.WriteLine(rectangle);
            CollisionRectangles.Add(new Rectangle((int)rectangle.x, (int)rectangle.y, (int)rectangle.width, (int)rectangle.height));
        }

    }
    /// <summary>
    /// Need to change this function to take multiple maps into account
    /// it's being pulled from the main Content/TileMaps directiory
    /// a fix might be putting each tilemap in a sub folder then getting the files from there
    /// </summary>
    /// <param name="Content"></param>
    private void LoadMap(ContentManager Content)
    {
        Map = new TiledMap($"{Content.RootDirectory}/TileMaps/{FolderName ??= string.Empty}{MapName}.tmx");
        Tilesets = Map.GetTiledTilesets($"{Content.RootDirectory}/TileMaps/{FolderName ??= string.Empty}");

        TilesetTextures.AddRange(from tile in Tilesets
                                 select Content.Load<Texture2D>($"TileMaps/tileset"));
    }

    public void Update(GameTime gameTime)
    {
    }
    public void UnloadContent()
    {
        Data.Game.Content.UnloadAsset($"tilemaps/{MapName}.tmx");
        Data.Game.Content.UnloadAsset($"tilemaps/tileset");
        Data.Game.Content.UnloadAsset($"tilemaps/fizzleset.tsx");
        Trace.WriteLine("Content Unloaded");
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        var tileLayers = Map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
        foreach (var layer in tileLayers)
            for (var y = 0; y < layer.height; y++)
                for (var x = 0; x < layer.width; x++)
                {
                    var index = (y * layer.width) + x;
                    var gid = layer.data[index];
                    var tileX = x * Map.TileWidth;
                    var tileY = y * Map.TileHeight;

                    if (gid is 0)
                        continue;

                    var mapTileset = Map.GetTiledMapTileset(gid);
                    var tileset = Tilesets[mapTileset.firstgid];
                    var rectangle = Map.GetSourceRect(mapTileset, tileset, gid);
                    var source = new Rectangle(rectangle.x, rectangle.y, rectangle.width, rectangle.height);
                    var destination = new Rectangle(tileX, tileY, Map.TileWidth, Map.TileHeight);

                    Translation tileTranslation = Translation.None;
                    if (Map.IsTileFlippedHorizontal(layer, x, y)) tileTranslation |= Translation.Flip_H;
                    if (Map.IsTileFlippedVertical(layer, x, y)) tileTranslation |= Translation.Flip_V;
                    if (Map.IsTileFlippedDiagonal(layer, x, y)) tileTranslation |= Translation.Flip_D;
                    SpriteEffects effects = SpriteEffects.None;
                    double rotation = 0f;

                    switch (tileTranslation)
                    {
                        default: break;
                        case Translation.Flip_H: effects = SpriteEffects.FlipHorizontally; break;
                        case Translation.Flip_V: effects = SpriteEffects.FlipVertically; break;

                        case Translation.Rotate_90:
                            rotation = Math.PI * .5f;
                            destination.X += Map.TileWidth;
                            break;
                        case Translation.Rotate_180:
                            rotation = Math.PI;
                            destination.X += Map.TileWidth;
                            destination.Y += Map.TileHeight;
                            break;
                        case Translation.Rotate_270:
                            rotation = Math.PI * 3 / 2;
                            destination.Y += Map.TileHeight;
                            break;
                        case Translation.Rotate_90AndFlip_H:
                            effects = SpriteEffects.FlipHorizontally;
                            rotation = Math.PI * .5f;
                            destination.X += Map.TileWidth;
                            break;
                    }
                    foreach (var ts in TilesetTextures)
                        spriteBatch.Draw(ts, destination, source, Color.White, (float)rotation, Vector2.Zero, effects, 0);
                }

    }
    public void DrawRectangleColliders(SpriteBatch spriteBatch)
    {
        foreach (var rect in CollisionRectangles)
        {
            spriteBatch.DrawRectangle(rect, Color.Red, 1);

        }

    }
}