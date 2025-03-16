using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public enum MouseButton
    {
        Right,Left,Middle
    }

    public class MouseAction : InputAction
    {
        private MouseButton _button;
        private ButtonState _buttonState;

        public MouseAction(MouseButton button, ButtonState buttonState) : this(button, buttonState, EventArgs.Empty) { }
      
        public MouseAction(MouseButton button, ButtonState buttonState,EventArgs eventArgs) : base(eventArgs)
        {
            _button = button;
            _buttonState = buttonState;
        }

        public override void CheckAction()
        {
            switch (_button)
            {
                case MouseButton.Right:
                    if (Mouse.GetState().RightButton == _buttonState)
                    {
                        ExecuteAction(EventArgs.Empty);
                    }
                    break;
                case MouseButton.Left:
                    if (Mouse.GetState().LeftButton == _buttonState)
                    {
                        ExecuteAction(EventArgs.Empty);
                    }
                    break;
                case MouseButton.Middle:
                    if (Mouse.GetState().MiddleButton == _buttonState)
                    {
                        ExecuteAction(EventArgs.Empty);
                    }
                    break;
            }   
        }
    }
}
