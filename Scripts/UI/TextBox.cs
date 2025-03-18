using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;


namespace Slay_The_Basilisk_MonoGame
{
    public class TextBox : UIElement
    {
        private string _text;
        private SpriteFont _font;
        private Color _textColor;
        private Rectangle _area;

        public TextBox(Vector2 position,int areaWidth, int areaHeight, float scale,string text,Color textColor) : base(position,scale, Color.White, null) 
        {
            _text = text;
            _textColor = textColor;
            _area = new Rectangle { X = (int)_position.X, Y = (int)_position.Y, Width= areaWidth, Height = areaHeight };
            _font = AssetsManager.Font;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float x = (_area.X + (_area.Width / 2) - ((_font.MeasureString(_text).X / 2) * _scale));
            float y = (_area.Y + (_area.Height / 2) - ((_font.MeasureString(_text).Y / 2) * _scale));

            spriteBatch.DrawString(_font, _text, new Vector2(x, y), _textColor, 0, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }

        public void UpdateText(string text)
        {
            if (_text != null) 
            {
                _text = text;
            }
        }

    }
}
