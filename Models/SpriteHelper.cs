using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fizzle.Models
{
    public abstract class SpriteHelper : IHitboxHelper, ISpriteCollision
    {
        Color IHitboxHelper.Color { get; set; }
        bool IHitboxHelper.Visible { get; set; } = true;
        Rectangle IHitboxHelper.Hitbox { get; set; }
        bool ISpriteCollision.Contact { get; set; }
        Vector2 ISpriteCollision.Velocity { get; set; }
    }
}
