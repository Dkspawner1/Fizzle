using MonoGame.Extended.Sprites;
using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Fizzle.Models
{
    public class Player : Sprite
    {
        public bool MainPlayer { get; }
        public PlayerController Controller { get; set; }
        public IHitbox HitboxHelper => this;
        public SpriteAABBCollision Collider => this;

        public Player(string pathToSF, float scale, Vector2 startPosition, bool mainPlayer, int controlScheme)
            : base(pathToSF, scale, startPosition)
        {
            Controller = new PlayerController(controlScheme);
            MainPlayer = mainPlayer;
            HitboxHelper.Color = Color.Red;
        }

        public void Update(GameTime gameTime)
        {
            Controller.Update();
            sprite.Update(gameTime);
            sprite.Play(animation);

            Position += Velocity;
            Hitbox = UpdateHitBox();

            Velocity = Vector2.Zero;

            var walkSpeed = Data.Game.TotalSeconds * 128f;
            Move(walkSpeed);
        }

        private Rectangle UpdateHitBox() => new Rectangle((int)Position.X, (int)Position.Y, sprite.TextureRegion.Width, sprite.TextureRegion.Height);

        private string animation = "idleDown";

        private void Move(float speed)
        {
            if (!CanMove)
                return;

            UpdateAnimation();

            switch (Controller.Direction)
            {
                case var dir when dir == -Vector2.UnitY: // Up
                    Velocity.Y = -speed;
                    break;
                case var dir when dir == Vector2.UnitY: // Down
                    Velocity.Y = speed;
                    break;
                case var dir when dir == -Vector2.UnitX: // Left
                    Velocity.X = -speed;
                    break;
                case var dir when dir == Vector2.UnitX: // Right
                    Velocity.X = speed;
                    break;
            }
        }

        private void UpdateAnimation()
        {
            switch (Controller.Direction)
            {
                case var dir when dir == -Vector2.UnitY:
                    animation = "idleUp";
                    break;
                case var dir when dir == Vector2.UnitY:
                    animation = "idleDown";
                    break;
                case var dir when dir == -Vector2.UnitX:
                    animation = "idleLeft";
                    break;
                case var dir when dir == Vector2.UnitX:
                    animation = "idleRight";
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
