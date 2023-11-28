using System.Linq;

namespace Fizzle.Managers;

public static class InputManager
{
    #region mouse
    public static MouseState mouse, oldMouse;
    public static Rectangle mouseRect;

    public static void UpdateMouse()
    {
        oldMouse = mouse;
        mouse = Mouse.GetState();
        mouseRect = new Rectangle(mouse.X, mouse.Y, 1, 1);
    }
    #endregion

    #region keyboard
    private static Vector2 direction;
    public static Vector2 Direction => direction;
    public static bool Moving => direction != Vector2.Zero;
    public static KeyboardState KeyboardState, OldKeyboardState;


    public static Dictionary<string, Keys> binds;


    public static void UpdateKeyboard()
    {
        OldKeyboardState = KeyboardState;
        KeyboardState = Keyboard.GetState();

        direction = Vector2.Zero;

        if (KeyboardState.IsKeyDown(binds["left"])) direction.X--;
        else if (KeyboardState.IsKeyDown(binds["right"])) direction.X++;
        else if (KeyboardState.IsKeyDown(binds["up"])) direction.Y--;
        else if (KeyboardState.IsKeyDown(binds["down"])) direction.Y++;
        else direction = Vector2.Zero;

    }

    #endregion
}