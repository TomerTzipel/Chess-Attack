using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOut.Input
{

    public class KeyboardAction : InputAction
    {
        private Keys _key;
        private KeyboardState _priorState;
        private KeyboardState _currentState;
        public KeyboardAction(Keys key, ButtonState buttonState) : this(key, buttonState, EventArgs.Empty) { }
        
        public KeyboardAction(Keys key, ButtonState buttonState, EventArgs eventArgs) : base(eventArgs)
        {
            _key = key;
        }
        public override void CheckAction()
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
