
namespace ChessOut.Scenes
{
    //A scene that has buttons, and serves as a basic menu
    public abstract class MenuScene : Scene
    {
        protected Button[] _buttons;

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in _buttons)
            {
                button.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Button button in _buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }
        }
    }
}
