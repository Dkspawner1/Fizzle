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
    public static KeyboardState kb, oldKb;
    public static void UpdateKeyboard()
    {
        oldKb = kb;
        kb = Keyboard.GetState();
    }
    #endregion
}