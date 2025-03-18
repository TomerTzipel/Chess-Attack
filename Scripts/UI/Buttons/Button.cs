
namespace ChessOut.UI
{
    public class Button : UIElement,IMyUpdateable
    {
        private string _text;
        private SpriteFont _font;
        private Color _textColor;

        private MouseState _previousMouseState;
        private MouseState _currentMouseState;

        private bool _isHovering;
        private Color _unhoveredColor;
        private Color _hoveredColor;

        public bool IsClickable { get; set; } = true;
        private Rectangle Area
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, (int)(_sprite.Width * _scale), (int)(_sprite.Height * _scale));
            }
        }

        public event EventHandler Click;

        public Button(Vector2 position,float scale, Texture2D sprite, string text) : this(position, scale, sprite, text, Color.Gray, Color.White, Color.Black) { }
        public Button(Vector2 position, Texture2D sprite, string text) : this(position,1f, sprite, text, Color.Gray, Color.White, Color.Black) { }

        public Button(Vector2 position,float scale, Texture2D sprite, string text, Color hoverColor, Color unhoveredColor,Color textColor) : base(position,scale, unhoveredColor, sprite)
        {
            _font = AssetsManager.Font;
            _text = text;
            _unhoveredColor = unhoveredColor;
            _hoveredColor = hoverColor;
            _textColor = textColor;
        }
     
        public void Update(GameTime gameTime)
        {
            if(!IsClickable) return;  
            
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
            _isHovering = false;
            CheckHovering();
            CheckActivation();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_isHovering)
            {
                _color = _hoveredColor;
            }
            else
            {
                _color = _unhoveredColor;
            }

            if(!IsClickable) _color = Color.DarkRed;


            base.Draw(gameTime, spriteBatch);

            if (string.IsNullOrEmpty(_text)) return;

            float x = (Area.X + (Area.Width / 2) - ((_font.MeasureString(_text).X / 2)*_scale));
            float y = (Area.Y + (Area.Height / 2) - ((_font.MeasureString(_text).Y / 2) * _scale));

            spriteBatch.DrawString(_font, _text, new Vector2(x, y), _textColor,0,Vector2.Zero,_scale,SpriteEffects.None,0f);
        }

        private void CheckHovering()
        {
            if (Area.Contains(_currentMouseState.X, _currentMouseState.Y))
            {
                _isHovering |= true;
            }
        }
        private void CheckActivation()
        {
            if(_isHovering && _currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed)
            {
                Click?.Invoke(this,EventArgs.Empty);
            }    
           
        }
    }
}
