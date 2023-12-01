using ImGuiNET;
using MonoGame.Extended.Sprites;
using System.Diagnostics;

namespace Fizzle.Models
{
    public class Player<controller> : Sprite where controller : PlayerController, new()
    {
        public bool MainPlayer { get; }
        public int ControlScheme { get; }

        public controller Controller { get; set; } = new();
        private IHitboxHelper HitboxHelper => this;
        private ISpriteCollision Collider => this;

        public Player(string pathToSF, float scale, Vector2 startPosition, bool mainPlayer, int controlScheme) : base(pathToSF, scale, startPosition)
        {
            MainPlayer = mainPlayer;
            Controller.AddController(controlScheme);
        }

        public override void LoadContent(ContentManager Content) => base.LoadContent(Content);

        public void Update(GameTime gameTime)
        {
            Controller.Update();

            UpdateAnimation();
            sprite.Update(gameTime);
            sprite.Play(animation).Play();

            var walkSpeed = Data.Game.TotalSeconds * 128f;
            Move(walkSpeed);
            Position += Velocity;
            Velocity = Vector2.Zero;

            HitboxHelper.Hitbox = Hitbox;
            HitboxHelper.Color = Color.Red;
            Collider.Velocity = Velocity;
        }

        private string lastDirection = "down", animation = "down";
        private void UpdateAnimation()
        {
            switch (Controller.Direction)
            {
                default:
                    animation = $"{lastDirection}";
                    break;
                case Vector2(1, 0):
                    animation = lastDirection = "right";
                    break;
                case Vector2(-1, 0):
                    animation = lastDirection = "left";
                    break;
                case Vector2(0, 1):
                    animation = lastDirection = "down";
                    break;
                case Vector2(0, -1):
                    animation = lastDirection = "up";
                    break;
            }
        }

        internal void Move(in float speed)
        {
            if (!CanMove)
                return;

            switch (Controller.Direction)
            {
                default:
                    Velocity = Vector2.Zero;
                    break;
                case Vector2(1, 0):
                    Velocity.X = speed;
                    break;
                case Vector2(-1, 0):
                    Velocity.X = -speed;
                    break;
                case Vector2(0, 1):
                    Velocity.Y = speed;
                    break;
                case Vector2(0, -1):
                    Velocity.Y = -speed;
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position, 0f, Scale);
            HitboxHelper.DrawHitbox(spriteBatch);
        }

    }
}
