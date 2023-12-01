using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Fizzle.Models
{
    public class Sprite : SpriteHelper
    {
        // Properties
        public Vector2 Scale { get; set; }
        public Vector2 Position { get; set; }
        public bool CanMove { get; set; }

        // Variables
        public int Hp;
        public int DamageLow, DamageHigh;
        public Vector2 Velocity;

        private string path;
        internal AnimatedSprite sprite;
        private SpriteSheet spritesheet;

        public double Damage() => new Random().Next(DamageLow, DamageHigh);

        public Rectangle Hitbox
        {
            get => new Rectangle
                ((int)(Position.X - (sprite.TextureRegion.Width / Scale.X / 2f) + 1f),
             (int)(Position.Y - sprite.TextureRegion.Height / Scale.Y / 2f + 1f),
                (int)(sprite.TextureRegion.Width / Scale.X),
                (int)(sprite.TextureRegion.Height / Scale.Y)); private set => Hitbox = value;
        }

        public Sprite(string pathToSF, float scale, Vector2 startPosition)
        {
            Scale = new(scale, scale);
            path = pathToSF;
            Position = startPosition;
            CanMove = true;


            Trace.WriteLine("Sprite Created");
        }
        ~Sprite()
        {
            Trace.WriteLine("Sprite Destroyed");
        }
        public virtual void LoadContent(ContentManager Content)
        {
            spritesheet = Content.Load<SpriteSheet>(path, new JsonContentLoader());
            sprite = new AnimatedSprite(spritesheet);

        }

    }
}
