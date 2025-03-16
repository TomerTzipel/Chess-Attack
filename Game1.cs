using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slay_The_Basilisk_MonoGame
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
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            AssetsManager.LoadContent(Content);
            GameManager.StartGame(this);
        }

        protected override void Update(GameTime gameTime)
        {;
            // TODO: Add your update logic here
            GameManager.UpdateCurrentScene(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();
            // TODO: Add your drawing code here
            //SpriteBatch.Draw(AssetsManager.PlayerSprite, new Vector2(-32f,-32f), new Rectangle { X = 0,Y=0,Width=64,Height=64},Color.White,0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None,0f);
            GameManager.DrawCurrentScene(gameTime);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
