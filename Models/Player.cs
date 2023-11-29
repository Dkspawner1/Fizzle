using MonoGame.Extended.Sprites;

namespace Fizzle.Models
{
    public class Player<Controller> : Sprite where Controller : PlayerController, new()
    {
        public bool MainPlayer { get; }

        public readonly Controller controller;

        public Player(string pathToSF, float scale, Vector2 startPosition, bool mainPlayer) : base(pathToSF, scale, startPosition)
        {
            MainPlayer = mainPlayer;
            controller = new Controller();
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
        }

        public void Update(GameTime gameTime)
        {



            controller.Update();

            UpdateAnimation();
            sprite.Update(gameTime);
            sprite.Play(animation).Play();

            var walkSpeed = Data.Game.TotalSeconds * 128f;
            Move(walkSpeed);
            Position += Velocity;
            Velocity = Vector2.Zero;


        }

        private string lastDirection = "down", animation = "down";
        private void UpdateAnimation()
        {
            switch (controller.Direction)
            {
                default:
                    animation = $"{lastDirection}";
                    break;
                case Vector2(1, 0):
                    animation = lastDirection = "right";
                    break;
                case Vector2(-1, 0):
                    animation = lastDirection = "left";
                    break;
                case Vector2(0, 1):
                    animation = lastDirection = "down";
                    break;
                case Vector2(0, -1):
                    animation = lastDirection = "up";
                    break;
            }
        }


        internal void Move(in float speed)
        {
            if (!CanMove)
                return;

            switch (controller.Direction)
            {
                default:
                    Velocity = Vector2.Zero;
                    break;
                case Vector2(1, 0):
                    Velocity.X = speed;
                    break;
                case Vector2(-1, 0):
                    Velocity.X = -speed;
                    break;
                case Vector2(0, 1):
                    Velocity.Y = speed;
                    break;
                case Vector2(0, -1):
                    Velocity.Y= -speed;
                    break;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position, 0f, Scale);
        }
    }
}
