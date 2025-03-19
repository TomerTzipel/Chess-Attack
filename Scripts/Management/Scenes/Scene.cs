
namespace ChessOut.Scenes
{
   //A class handling what is currently drawn,updates and active player input
    public abstract class Scene : IMyDrawable,IMyUpdateable
    {
        protected InputMap _inputMap;

        public Scene() 
        {
            _inputMap = new InputMap();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public virtual void EnterScene() { }     
    }
}
