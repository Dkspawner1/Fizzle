using MonoGame.ImGuiNet;

namespace Fizzle.Core;

public static class Data
{
    public struct Window
    {
        public static string Title { get; set; } = "Fizzle's Game!";
        public static int ScreenW { get; set; } = 1600;
        public static int ScreenH { get; set; } = 900;
        public static bool Exit { get; set; }
        public bool IsFullscreen { get; set; }

    }
    public struct Game
    {
        public enum GameStates { Menu, Game, Settings }
        public static GameStates CurrentState { get; set; } = GameStates.Menu;
        public static ContentManager Content { get; set; }
    }
    public struct GUI
    {
        public static ImGuiRenderer Renderer { get; set; }
    }
}