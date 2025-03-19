
namespace ChessOut.UI
{
    //A text box to draw text to the screen
    public class TextBox : UIElement
    {
        private string _text;
        private SpriteFont _font;
        private Rectangle _area;
        public Color TextColor { get; set; }
        public TextBox(Vector2 position,int areaWidth, int areaHeight, float scale,string text,Color textColor) : base(position,scale, Color.White, null) 
        {
            _text = text;
            TextColor = textColor;
            _area = new Rectangle { X = (int)_position.X, Y = (int)_position.Y, Width= areaWidth, Height = areaHeight };
            _font = AssetsManager.Font;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Centers the text to the rectangle _area
            float x = (_area.X + (_area.Width / 2) - ((_font.MeasureString(_text).X / 2) * _scale));
            float y = (_area.Y + (_area.Height / 2) - ((_font.MeasureString(_text).Y / 2) * _scale));

            spriteBatch.DrawString(_font, _text, new Vector2(x, y), TextColor, 0, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }

        //Change what is wrriten in the text
        public void UpdateText(string text)
        {
            if (_text != null) 
            {
                _text = text;
            }
        }

    }
}
