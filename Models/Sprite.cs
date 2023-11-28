using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using System;
using Fizzle.Managers;


namespace Fizzle.Models
{
    internal class Sprite : IFizzleComponent, ISpriteCollision
    {
        internal enum ControlScheme : byte
        {
            WASD = 0x1,
            ARROW_KEYS = 0x2,
            CUSTOM = 0x4,
            NONE = 0x8
        }
        public Vector2 Position { get; set; }
        public bool MainSprite { get; }
        public Vector2 Scale { get; set; }
        public bool CanMove { get; set; } = true;
        public Rectangle PlayerRectangle { get; set; }

        public int Hp;
        public int DamageLow, DamageHigh;


        private string path;
        private AnimatedSprite sprite;
        private SpriteSheet spritesheet;
        private Vector2 Velocity;

        public double Damage() => new Random().Next(DamageLow, DamageHigh);

        public Sprite(string pathToSF, float scale, Vector2 startPosition, bool mainSprite)
        {
            Position = startPosition;
            MainSprite = mainSprite;
            Scale = new(scale, scale);

            path = pathToSF;
            LoadControl(ControlScheme.WASD);
        }
        ~Sprite()
        {

        }

        private void LoadControl(ControlScheme control)
        {
            InputManager.binds = new();

            switch (control)
            {
                case ControlScheme.WASD:
                    InputManager.binds.Add("up", Keys.W);
                    InputManager.binds.Add("down", Keys.S);
                    InputManager.binds.Add("left", Keys.A);
                    InputManager.binds.Add("right", Keys.D);
                    break;
                case ControlScheme.ARROW_KEYS:
                    InputManager.binds.Add("up", Keys.Up);
                    InputManager.binds.Add("down", Keys.Down);
                    InputManager.binds.Add("left", Keys.Left);
                    InputManager.binds.Add("right", Keys.Right);
                    break;
                case ControlScheme.CUSTOM:
                    InputManager.binds["up"] = Keys.G;
                    InputManager.binds["down"] = Keys.N;
                    InputManager.binds["left"] = Keys.J;
                    InputManager.binds["right"] = Keys.K;
                    break;
                case ControlScheme.NONE:
                    break;
            }
        }


        public void LoadContent(ContentManager Content)
        {
            spritesheet = Content.Load<SpriteSheet>(path, new JsonContentLoader());
            sprite = new AnimatedSprite(spritesheet);

        }
        public void Update(GameTime gameTime)
        {
            if (Hp <= 0) { /*Die*/}


            float walkSpeed = Data.Game.TotalSeconds* 128;
            Move(walkSpeed);


            Position += Velocity;
            Velocity = Vector2.Zero;

            sprite.Update(gameTime);
            sprite.Play(animation).Play();
                

        }

        private string lastDirection = "down", animation = "down";

        internal void Move(float speed)
        {
            if (!CanMove)
                return;

            bool conditionalState = InputManager.Moving;

            switch (InputManager.Direction)
            {
                default:
                    animation = $"{lastDirection}";

                    break;
                case Vector2(1, 0):
                    animation = lastDirection = "right";
                    Velocity.X = speed;


                    break;
                case Vector2(-1, 0):
                    animation = lastDirection = "left";
                    Velocity.X = -speed;

                    break;
                case Vector2(0, 1):
                    animation = lastDirection = "down";
                    Velocity.Y = speed;

                    break;
                case Vector2(0, -1):
                    animation = lastDirection = "up";
                    Velocity.Y = -speed;
                    break;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position, 0f, Scale);
        }
    }
}
