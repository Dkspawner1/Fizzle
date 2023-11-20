using System.Diagnostics;
using Fizzle.Managers;
using Microsoft.Xna.Framework.Media;

namespace Fizzle.Scenes;

public class MenuScene : IFizzleComponent
{
    private List<Texture2D> buttons;
    private List<Rectangle> buttonRects;
    private List<Music> songs = new List<Music>();
    public MenuScene()
    {
        buttons = new List<Texture2D>(3);
        buttonRects = new List<Rectangle>(buttons.Capacity);

    }

    public void LoadContent(ContentManager Content)
    {
        const int START_X = 5, INCREMENT_VALUE = 200, SCALE = 4;

        for (int i = 0; i < buttons.Capacity; i++)
        {
            buttons.Add(Content.Load<Texture2D>($"Textures/btn{i}"));
            buttonRects.Add(new Rectangle(START_X, (Data.Window.ScreenH / 2) - (buttons[i].Height / 2) + (i * INCREMENT_VALUE), buttons[i].Width / SCALE, buttons[i].Height / SCALE));

        }
        songs.Add(new Music(Content.Load<Song>("music/funk"), true, 0.1f));
    }

    public void Update(GameTime gameTime)
    {

        InputManager.UpdateMouse();

        if (InputManager.mouse.LeftButton == ButtonState.Pressed && InputManager.mouseRect.Intersects(buttonRects[0]))
            Data.Game.CurrentState = Data.Game.GameStates.Game;

        if (InputManager.mouse.LeftButton == ButtonState.Pressed && InputManager.mouseRect.Intersects(buttonRects[2]))
            Data.Window.Exit = true;
        songs[0].Update();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        for (int i = 0; i < buttons.Count; i++)
        {
            if (InputManager.mouseRect.Intersects(buttonRects[i]))
                spriteBatch.Draw(buttons[i], buttonRects[i], Color.Gray);
            else spriteBatch.Draw(buttons[i], buttonRects[i], Color.White);

        }
        spriteBatch.End();
    }
}
