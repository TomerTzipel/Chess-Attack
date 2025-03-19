
namespace ChessOut.UI
{
    //A father class for UI elements, can draw a sprite
    public class UIElement : IMyDrawable
    {
        protected Vector2 _position;
        protected float _scale;
        protected Color _color;
        protected Texture2D _sprite;
        
        public UIElement(Vector2 position,float scale, Color color, Texture2D sprite)
        {
            _position = position;
            _color = color;
            _sprite = sprite;
            _scale = scale;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_sprite == null) return;
            spriteBatch.Draw(_sprite, _position,new Rectangle { X=0,Y=0, Width=_sprite.Width, Height=_sprite.Height }, _color,0f,Vector2.Zero,_scale,SpriteEffects.None,0f);
        }
    }
}
