

namespace ChessOut
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            AssetsManager.LoadContent(Content);
            GameManager.StartGame(this);
        }

        protected override void Update(GameTime gameTime)
        {
            GameManager.UpdateCurrentScene(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();
            GameManager.DrawCurrentScene(gameTime);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
