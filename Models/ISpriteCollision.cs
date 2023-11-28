
namespace Fizzle.Models
{
    public interface ISpriteCollision
    {
        #region collision
        protected virtual bool IsTouchingLeft(ref Vector2 velocity,Rectangle target, Rectangle player) =>
            player.Right + velocity.X > target.Left &&
                player.Left < target.Left &&
                player.Bottom > target.Top &&
                player.Top < target.Bottom;
        protected virtual bool IsTouchingRight(ref Vector2 velocity, Rectangle target, Rectangle player) =>
          player.Left + velocity.X > target.Right &&
              player.Right < target.Right &&
              player.Bottom > target.Top &&
              player.Top < target.Bottom;
        protected virtual bool IsTouchingTop(ref Vector2 velocity,Rectangle target, Rectangle player) =>
          player.Bottom + velocity.Y > target.Top &&
              player.Top < target.Top &&
              player.Right > target.Left &&
              player.Left < target.Right;
        protected virtual bool IsTouchingBottom(ref Vector2 velocity, Rectangle target, Rectangle player) =>
          player.Top + velocity.Y > target.Top &&
              player.Bottom < target.Bottom &&
              player.Right > target.Left &&
              player.Left < target.Right;


        #endregion
    }
}
