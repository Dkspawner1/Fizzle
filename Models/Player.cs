using MonoGame.Extended.Sprites;
using System;
using System.Diagnostics;
using System.Threading;

namespace Fizzle.Models
{
    public class Player : Sprite
    {
        public bool MainPlayer { get; }
        public int ControlScheme { get; }
        public PlayerController Controller { get; set; } = new();
        public IHitbox HitboxHelper => this;
        public SpriteAABBCollision Collider => this;


        public Player(string pathToSF, float scale, Vector2 startPosition, bool mainPlayer, int controlScheme) : base(pathToSF, scale, startPosition)
        {
            MainPlayer = mainPlayer;
            Controller.AddController(controlScheme);
            HitboxHelper.Color = Color.Red;
        }

        public override void LoadContent(ContentManager Content) => base.LoadContent(Content);

        public void Update(GameTime gameTime)
        {
            Controller.Update();

            sprite.Update(gameTime);
            sprite.Play(animation).Play();

            Position += Velocity;
            Hitbox = UpdateHitBox();

            Velocity = Vector2.Zero;

            var walkSpeed = Data.Game.TotalSeconds * 128f;
            Move(walkSpeed);

        }

        private Rectangle UpdateHitBox() => new Rectangle((int)Position.X, (int)Position.Y, sprite.TextureRegion.Width, sprite.TextureRegion.Height);

        private string lastDirection = "down", animation = "down";
        private void Move(float speed)
        {
            if (!CanMove)
                return;

            switch (Controller.Direction)
            {
                default:
                    animation = $"{lastDirection}";
                    break;
                case Vector2(0, -1):
                    Velocity.Y = -speed;
                    animation = lastDirection = "up";
                    break;
                case Vector2(0, 1):
                    Velocity.Y = speed;
                    animation = lastDirection = "down";
                    break;
                case Vector2(-1, 0):
                    Velocity.X = -speed;
                    animation = lastDirection = "left";
                    break;
                case Vector2(1, 0):
                    Velocity.X = speed;
                    animation = lastDirection = "right";
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
