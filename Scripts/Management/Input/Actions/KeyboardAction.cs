

namespace ChessOut.Input
{
    //An InputAction that checks for triggers from the keyboard.
    //Actions are active only if in the current state the trigger is perssed and in the last it was released
    public class KeyboardAction : InputAction
    {
        private Keys _key;
        private KeyboardState _priorState;
        private KeyboardState _currentState;
        public KeyboardAction(Keys key) : this(key, EventArgs.Empty) { }
        
        public KeyboardAction(Keys key, EventArgs eventArgs) : base(eventArgs)
        {
            _key = key;
        }
        public override void CheckIfActionTriggered()
        {
            _priorState = _currentState;
            _currentState = Keyboard.GetState();
            if (_currentState.IsKeyDown(_key) && _priorState.IsKeyUp(_key))
            {
                ExecuteAction(_eventArgs);
            }
        }
    }
}
