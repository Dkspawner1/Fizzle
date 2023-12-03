namespace Fizzle.Models
{

    public abstract class ISpriteCollision
    {
        public Vector2 Velocity;
        #region collision
        internal virtual bool IsTouchingLeft(Rectangle player, Vector2 Velocity, Rectangle target) =>
            player.Right + Velocity.X > target.Left &&
                player.Left < target.Left &&
                player.Bottom > target.Top &&
                player.Top < target.Bottom;
        internal virtual bool IsTouchingRight(Rectangle player, Vector2 Velocity, Rectangle target) =>
          player.Left + Velocity.X < target.Right &&
              player.Right > target.Right &&
              player.Bottom > target.Top &&
              player.Top < target.Bottom;
        internal virtual bool IsTouchingTop(Rectangle player, Vector2 Velocity, Rectangle target) =>
          player.Bottom + Velocity.Y > target.Top &&
              player.Top < target.Top &&
              player.Right > target.Left &&
              player.Left < target.Right;
        internal virtual bool IsTouchingBottom(Rectangle player, Vector2 Velocity, Rectangle target) =>
          player.Top + Velocity.Y < target.Bottom &&
              player.Bottom > target.Bottom &&
              player.Right > target.Left &&
              player.Left < target.Right;
        #endregion
    }
}
