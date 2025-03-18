
namespace ChessOut.Scenes
{
   
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
