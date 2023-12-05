using Fizzle.Tile;

namespace Fizzle.Models
{
    public abstract class SpriteAABBCollision
    {
        public Vector2 Velocity;
        #region collision
        internal bool IsTouchingLeft(Rectangle player, Rectangle target) =>
            player.Right + Velocity.X > target.Left &&
            player.Left < target.Left &&
            player.Bottom > target.Top &&
            player.Top < target.Bottom;
        internal bool IsTouchingRight(Rectangle player, Rectangle target) =>
            player.Left + Velocity.X < target.Right &&
            player.Right > target.Right &&
            player.Bottom > target.Top &&
            player.Top < target.Bottom;
        internal bool IsTouchingTop(Rectangle player, Rectangle target) =>
            player.Bottom + Velocity.Y > target.Top &&
            player.Top < target.Top &&
            player.Right > target.Left &&
            player.Left < target.Right;
        internal bool IsTouchingBottom(Rectangle player, Rectangle target) =>
            player.Top + Velocity.Y < target.Bottom &&
            player.Bottom > target.Bottom &&
            player.Right > target.Left &&
            player.Left < target.Right;
        internal void CheckPlayerCollision(IHitbox hitbox, Rectangle target)
        {
            if (Velocity.X > 0 && IsTouchingLeft(hitbox.Hitbox, target) || IsTouchingRight(hitbox.Hitbox, target))
                Velocity.X = 0;

            if (Velocity.Y > 0 && IsTouchingTop(hitbox.Hitbox, target) || Velocity.Y < 0 && IsTouchingBottom(hitbox.Hitbox, target))
                Velocity.Y = 0;
        }
        internal void CheckMapCollision<TMM>(IHitbox player, TMM currentMap) where TMM : TileMapManager, new()
        {
            foreach (var rectangle in currentMap.CurrentMap.CollisionRectangles)
            {
                if (Velocity.X > 0 && IsTouchingLeft(player.Hitbox, rectangle) || IsTouchingRight(player.Hitbox, rectangle))
                    Velocity.X = 0;
                if (Velocity.Y > 0 && IsTouchingTop(player.Hitbox, rectangle) || Velocity.Y < 0 && IsTouchingBottom(player.Hitbox, rectangle))
                    Velocity.Y = 0;
            }

        }
        #endregion
    }
}
