
namespace ChessOut.Input
{
    //An InputAction that checks for triggers from the mouse.
    //Actions are active only if in the current state the trigger is perssed and in the last it was released
    public class MouseAction : InputAction
    {
        private MouseButton _button;

        private MouseState _priorState;
        private MouseState _currentState;

        public MouseAction(MouseButton button) : this(button, EventArgs.Empty) { }
      
        public MouseAction(MouseButton button,EventArgs eventArgs) : base(eventArgs)
        {
            _button = button;
        }

        public override void CheckIfActionTriggered()
        {
            _priorState = _currentState;
            _currentState = Mouse.GetState();

            switch (_button)
            {
                case MouseButton.Right:
                    if (_currentState.RightButton == ButtonState.Pressed && _priorState.RightButton == ButtonState.Released)
                    {
                        ExecuteAction(EventArgs.Empty);
                    }
                    break;
                case MouseButton.Left:
                    if (_currentState.LeftButton == ButtonState.Pressed && _priorState.LeftButton == ButtonState.Released)
                    {
                        ExecuteAction(EventArgs.Empty);
                    }
                    break;
                case MouseButton.Middle:
                    if (_currentState.MiddleButton == ButtonState.Pressed && _priorState.MiddleButton == ButtonState.Released)
                    {
                        ExecuteAction(EventArgs.Empty);
                    }
                    break;
            }   
        }
    }
}
