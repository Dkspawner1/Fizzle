using MonoGame.Extended;

namespace Fizzle.Models
{
    public interface IHitboxHelper
    {
        public Rectangle @Hitbox { get; set; }
        public Color @Color { get; set; }
        protected bool Visible { get; set; } 
        
        public virtual void DrawHitbox(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;

            spriteBatch.DrawRectangle(Hitbox, Color, 1f);
        }
    }
}
