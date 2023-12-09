using Fizzle.Tile;

namespace Fizzle.Models
{
    public abstract class SpriteAABBCollision
    {
        public Vector2 Velocity;
        #region collision
        internal bool IsTouchingLeft(Rectangle sprite, Rectangle target) =>
            sprite.Right + Velocity.X > target.Left &&
            sprite.Left < target.Left &&
            sprite.Bottom > target.Top &&
            sprite.Top < target.Bottom;
        internal bool IsTouchingRight(Rectangle sprite, Rectangle target) =>
            sprite.Left + Velocity.X < target.Right &&
            sprite.Right > target.Right &&
            sprite.Bottom > target.Top &&
            sprite.Top < target.Bottom;
        internal bool IsTouchingTop(Rectangle sprite, Rectangle target) =>
            sprite.Bottom + Velocity.Y > target.Top &&
            sprite.Top < target.Top &&
            sprite.Right > target.Left &&
            sprite.Left < target.Right;
        internal bool IsTouchingBottom(Rectangle sprite, Rectangle target) =>
            sprite.Top + Velocity.Y < target.Bottom &&
            sprite.Bottom > target.Bottom &&
            sprite.Right > target.Left &&
            sprite.Left < target.Right;
        internal void CheckPlayerCollision<Sprite>(Sprite sprite, Rectangle target) where Sprite : IHitbox
        {
            if (Velocity.X > 0 && IsTouchingLeft(sprite.Hitbox, target) || IsTouchingRight(sprite.Hitbox, target))
                Velocity.X = 0;
            if (Velocity.Y > 0 && IsTouchingTop(sprite.Hitbox, target) || IsTouchingBottom(sprite.Hitbox, target))
                Velocity.Y = 0;
        }
        internal void CheckMapCollision<Sprite, MapManager>(Sprite sprite, MapManager currentMap) where MapManager : TileMapManager where Sprite : IHitbox
        {
            foreach (var rectangle in currentMap.CurrentMap.CollisionRectangles)
            {
                if (Velocity.X > 0 && IsTouchingLeft(sprite.Hitbox, rectangle) || IsTouchingRight(sprite.Hitbox, rectangle))
                    Velocity.X = 0;
                if (Velocity.Y > 0 && IsTouchingTop(sprite.Hitbox, rectangle) || IsTouchingBottom(sprite.Hitbox, rectangle))
                    Velocity.Y = 0;
            }
        }
        #endregion
    }
}
