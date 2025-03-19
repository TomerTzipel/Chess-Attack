
namespace ChessOut.UI
{
    //A clickable button that trigger a click event
    //Shows then the button is hovered or when it is unclickble
    public class Button : UIElement,IMyUpdateable
    {
        private string _text;
        private SpriteFont _font;
        private Color _textColor;

        private MouseAction _clickAction;

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

        public event EventHandler OnClick;

        public Button(Vector2 position,float scale, Texture2D sprite, string text) : this(position, scale, sprite, text, Color.Gray, Color.White, Color.Black) { }
        public Button(Vector2 position, Texture2D sprite, string text) : this(position,1f, sprite, text, Color.Gray, Color.White, Color.Black) { }
        public Button(Vector2 position,float scale, Texture2D sprite, string text, Color hoverColor, Color unhoveredColor,Color textColor) : base(position,scale, unhoveredColor, sprite)
        {
            _font = AssetsManager.Font;
            _text = text;
            _unhoveredColor = unhoveredColor;
            _hoveredColor = hoverColor;
            _textColor = textColor;

            //Sets up the LMB action
            _clickAction = new MouseAction(MouseButton.Left);
            _clickAction.Action += CheckButtonActivation;
        }
     
        public void Update(GameTime gameTime)
        {
            //Doesn't allow ativation if not clickable
            if(!IsClickable) return;

            //Checks for hovering and if the button was clicked
            _isHovering = false;
            CheckIsMouseHovering();
            _clickAction.CheckIfActionTriggered();
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

            //Draws the string if it isn't null
            if (string.IsNullOrEmpty(_text)) return;

            //Makes sure to draw the string in the center of the button
            float x = (Area.X + (Area.Width / 2) - ((_font.MeasureString(_text).X / 2)*_scale));
            float y = (Area.Y + (Area.Height / 2) - ((_font.MeasureString(_text).Y / 2) * _scale));

            spriteBatch.DrawString(_font, _text, new Vector2(x, y), _textColor,0,Vector2.Zero,_scale,SpriteEffects.None,0f);
        }

        private void CheckIsMouseHovering()
        {
            if (Area.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                _isHovering = true;
            }
        }

        private void CheckButtonActivation(object sender, EventArgs args)
        {   
            //Only if the mouse hovers the button when we click we invoke OnClick
            if (_isHovering)
            {
                OnClick?.Invoke(this,EventArgs.Empty);
            }   
        }
    }
}
