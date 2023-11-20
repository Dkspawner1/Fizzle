namespace Fizzle.Managers;

public static class InputManager
{
    public static MouseState mouse, oldMouse;
    public static Rectangle mouseRect;

    public static void UpdateMouse()
    {
        oldMouse = mouse;
        mouse = Mouse.GetState();
        mouseRect = new Rectangle(mouse.X, mouse.Y, 1, 1);
    }

}