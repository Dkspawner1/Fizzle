using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using System;


namespace Fizzle.Models
{
    public class Sprite : ISpriteCollision
    {
        public Vector2 Scale { get; set; }
        public Rectangle PlayerRectangle { get; set; }
        public Vector2 Position { get; set; }
        public bool CanMove { get; set; }

        public int Hp;
        public int DamageLow, DamageHigh;
        public Vector2 Velocity;

        private string path;
        internal AnimatedSprite sprite;
        private SpriteSheet spritesheet;




        public double Damage() => new Random().Next(DamageLow, DamageHigh);

        public Sprite(string pathToSF, float scale, Vector2 startPosition)
        {
            Scale = new(scale, scale);
            path = pathToSF;
            Position = startPosition;
            CanMove = true;
        }
        ~Sprite()
        {

        }
        public virtual void LoadContent(ContentManager Content)
        {
            spritesheet = Content.Load<SpriteSheet>(path, new JsonContentLoader());
            sprite = new AnimatedSprite(spritesheet);

        }
    
    }
}
