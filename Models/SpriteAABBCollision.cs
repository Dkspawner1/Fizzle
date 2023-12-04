using System.Runtime.CompilerServices;

namespace Fizzle.Models
{
    public abstract class SpriteAABBCollision
    {
        public Vector2 Velocity;
        #region collision
        internal virtual bool IsTouchingLeft(Rectangle player, Rectangle target) =>
            player.Right + Velocity.X > target.Left &&
                player.Left < target.Left &&
                player.Bottom > target.Top &&
                player.Top < target.Bottom;
        internal virtual bool IsTouchingRight(Rectangle player, Rectangle target) =>
          player.Left + Velocity.X < target.Right &&
              player.Right > target.Right &&
              player.Bottom > target.Top &&
              player.Top < target.Bottom;
        internal virtual bool IsTouchingTop(Rectangle player, Rectangle target) =>
          player.Bottom + Velocity.Y > target.Top &&
              player.Top < target.Top &&
              player.Right > target.Left &&
              player.Left < target.Right;
        internal virtual bool IsTouchingBottom(Rectangle player, Rectangle target)
        {
            return player.Top + Velocity.Y < target.Bottom &&
              player.Bottom > target.Bottom &&
              player.Right > target.Left &&
              player.Left < target.Right;

            //return player.Top + Velocity.Y < target.Bottom &&
            //  player.Bottom > target.Bottom &&
            //  player.Right > target.Left &&
            //  player.Left < target.Right;
        }

        internal void CheckAginstPlayerCollision(in Player player, Rectangle target)
        {
            
            if (Velocity.X > 0 && IsTouchingLeft(player.Hitbox, target) || IsTouchingRight(player.Hitbox, target))
                player.Velocity.X = 0;

            if (Velocity.Y > 0 && IsTouchingTop(player.Hitbox, target) || Velocity.Y < 0 && IsTouchingBottom(player.Hitbox, target))
                Velocity.Y = 0;
        }
        #endregion
    }
}
