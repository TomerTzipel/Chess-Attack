
namespace ChessOut.UI
{
    //A slide that goes from 0-1 depending on the float value
    public class PercentSlider : UIElement
    {
        private float _value;
        private Texture2D _fillSprite;
        public PercentSlider(Vector2 position, float scale, Color color, Texture2D sprite, Texture2D fillSprite,float value) : base(position, scale, color, sprite)
        {
            _value = value;
            _fillSprite = fillSprite;
        }
        public PercentSlider(Vector2 position,float scale, Color color, Texture2D sprite,Texture2D fillSprite) : this(position,scale, color, sprite, fillSprite,1f) { }
      
        public void SetValue(float value)
        {
            _value = Math.Clamp(value, 0f, 1f);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(_fillSprite, _position, new Rectangle { X = 0, Y = 0, Width = (int)(_sprite.Width * _value), Height = _sprite.Height }, _color, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }

    }
}
