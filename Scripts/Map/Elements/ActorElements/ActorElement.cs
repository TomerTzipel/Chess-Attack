

namespace ChessOut.MapSystem.Elements
{
    //An element that is drawble on the map
    public class ActorElement : MapElement, IMyDrawable
    {
        protected Texture2D _sprite;
        protected Point _mapPosition;
            
        protected Vector2 WorldPosition
        {
            get 
            {
                Camera camera = RunManager.CurrentLevel.Camera;
                return ((_mapPosition - camera.Origin) * MapElement.ElementSize) + camera.WorldPosition;
            }
        }
        public ActorElement(Texture2D sprite, Point mapPosition)
        {
            _sprite = sprite;
            _mapPosition = mapPosition;
        }

        public virtual void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, WorldPosition, Color.White);
        }
    }
}
