using System;
using System.Diagnostics;

namespace Fizzle.Models
{
    public class PlayerController
    {
        public enum ControlScheme : byte
        {
            WASD = 0x1,
            ARROW_KEYS = 0x2,
            CUSTOM = 0x4,
            NONE = 0x8
        }

        public Dictionary<string, Keys> Binds { get; set; }

        private Vector2 direction;
        public Vector2 Direction => direction;

        protected KeyboardState kb, oldKb;

        public PlayerController()
        {
            Binds = new Dictionary<string, Keys>();
            LoadKeybinds(ControlScheme.WASD);
        }

        private void LoadKeybinds(ControlScheme control)
        {
            switch (control)
            {
                case ControlScheme.WASD:
                    Binds[$"up"] = Keys.W;
                    Binds[$"down"] = Keys.S;
                    Binds[$"left"] = Keys.A;
                    Binds[$"right"] = Keys.D;
                    break;
                case ControlScheme.ARROW_KEYS:
                    Binds[$"up"] = Keys.Up;
                    Binds[$"down"] = Keys.Down;
                    Binds[$"left"] = Keys.Left;
                    Binds[$"right"] = Keys.Right;
                    break;
                case ControlScheme.CUSTOM:
                    break;
                case ControlScheme.NONE:
                    break;
            }
        }

        public void Update()
        {
            oldKb = kb;
            kb = Keyboard.GetState();

            direction = Vector2.Clamp(direction, Vector2.Zero, new Vector2(-1,-1));

            if (kb.IsKeyDown(Binds["left"])) direction.X--;
            else if (kb.IsKeyDown(Binds["right"])) direction.X++;
            else if (kb.IsKeyDown(Binds["up"])) direction.Y--;
            else if (kb.IsKeyDown(Binds["down"])) direction.Y++;
            else direction = Vector2.Zero;
        }

    }
}
