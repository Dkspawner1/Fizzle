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
        public KeyboardState kb, oldKb;
        public bool ControlSchemeInUse { get; set; } = false;

        public PlayerController()
        {
            Binds = new Dictionary<string, Keys>();
        }
        public void AddControlScheme()
        { 
        
        }

        public void LoadKeybinds(ControlScheme control)
        {
            switch (control)
            {
                default:
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

            NormalizeClamp(ref direction);
            if (kb.IsKeyDown(Binds["left"])) direction.X--;
            else if (kb.IsKeyDown(Binds["right"])) direction.X++;
            else if (kb.IsKeyDown(Binds["up"])) direction.Y--;
            else if (kb.IsKeyDown(Binds["down"])) direction.Y++;
            else direction = Vector2.Zero;
        }
        private void NormalizeClamp(ref Vector2 dir) => dir = Vector2.Clamp(dir, Vector2.Zero, Vector2.Negate(Vector2.One));
    }
}
