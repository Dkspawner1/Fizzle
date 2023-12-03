namespace Fizzle.Models
{
    public abstract class SpriteHelper : ISpriteCollision, IHitbox
    {
        public Color Color { get; set; }
        public bool Visible { get; set; } = true;
        public Rectangle Hitbox { get; set; }
    }
}
