namespace Fizzle.Models
{
    public abstract class SpriteHelper : SpriteAABBCollision, IHitbox
    {
        public Color Color { get; set; }
        public bool Visible { get; set; } = true;
        public Rectangle Hitbox { get; set; }
    }
}
