using ImGuiNET;

namespace Fizzle.Models
{
    public partial class PlayerController : ControlSchemes
    {
        public Dictionary<string, Keys> Binds { get; set; }

        private Vector2 direction;
        public Vector2 Direction => direction;

        private KeyboardState kb, oldKb;
        public PlayerController() => Binds = new Dictionary<string, Keys>();

        // Later on possibly make a function to swap keybinds
        public void AddController(int scheme)
        {
            switch (scheme)
            {
                default:
                case WASD:
                    Binds[$"up"] = Keys.W;
                    Binds[$"down"] = Keys.S;
                    Binds[$"left"] = Keys.A;
                    Binds[$"right"] = Keys.D;
                    break;
                case ARROW_KEYS:
                    Binds[$"up"] = Keys.Up;
                    Binds[$"down"] = Keys.Down;
                    Binds[$"left"] = Keys.Left;
                    Binds[$"right"] = Keys.Right;
                    break;
                case CUSTOM:
                    break;
                case NONE:
                    Binds[$"up"] = default;
                    Binds[$"down"] = default;
                    Binds[$"left"] = default;
                    Binds[$"right"] = default;
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

        public void DrawUI()
        {
            ImGui.BeginListBox($"Keybinds Player: ");
            foreach (var kvp in Binds)
                ImGui.Text($"{kvp.Key} = {kvp.Value}");
            ImGui.EndListBox();
        }
    }
}
