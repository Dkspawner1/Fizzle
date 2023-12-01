
namespace Fizzle.Models
{

    public interface ISpriteCollision
    {
        internal bool Contact { get; set; }
        internal Vector2 Velocity { get; set; }

        #region collision
        internal virtual bool IsTouchingLeft(Rectangle target, Rectangle player) =>
            player.Right + Velocity.X > target.Left &&
                player.Left < target.Left &&
                player.Bottom > target.Top &&
                player.Top < target.Bottom;
        internal virtual bool IsTouchingRight(Rectangle target, Rectangle player) =>
          player.Left + Velocity.X > target.Right &&
              player.Right < target.Right &&
              player.Bottom > target.Top &&
              player.Top < target.Bottom;
        internal virtual bool IsTouchingTop(Rectangle target, Rectangle player) =>
          player.Bottom + Velocity.Y > target.Top &&
              player.Top < target.Top &&
              player.Right > target.Left &&
              player.Left < target.Right;
        internal virtual bool IsTouchingBottom(Rectangle target, Rectangle player) =>
          player.Top + Velocity.Y > target.Top &&
              player.Bottom < target.Bottom &&
              player.Right > target.Left &&
              player.Left < target.Right;


        #endregion
    }
}
