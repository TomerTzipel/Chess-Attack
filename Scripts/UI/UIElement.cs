using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Slay_The_Basilisk_MonoGame
{
    public class UIElement : IMyDrawable
    {
        protected Vector2 _position; 
        protected Color _color;
        protected Texture2D _sprite;
        
        public UIElement(Vector2 position, Color color, Texture2D sprite)
        {
            _position = position;
            _color = color;
            _sprite = sprite;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, _position, _color);
        }
    }
}
