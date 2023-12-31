namespace Fizzle.Scenes
{
    public abstract class Scene
    {
        public ContentManager GlobalContent { get; }
        public ContentManager LocalContent { get; }
        public Scene(Game1 instance)
        {
            GlobalContent = instance.Content;
            LocalContent = new ContentManager(instance.Services);
        }
        public virtual void Unload()
        {
            LocalContent.Unload();
            LocalContent.Dispose();
        }
        public virtual void LoadContent(ContentManager Content){}
        public virtual void Update(GameTime gameTime)
        { }
        public virtual void Draw(SpriteBatch spriteBatch) { } 

    }
}
