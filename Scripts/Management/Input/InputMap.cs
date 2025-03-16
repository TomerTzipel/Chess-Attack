using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public class InputMap
    {
        private List<InputAction> _inputActions;

        public InputMap() 
        {
            _inputActions = new List<InputAction>(4);
        }

        public InputAction AddAction(InputAction inputAction)
        {
            _inputActions.Add(inputAction);
            return inputAction;
        }

        public void CheckActions()
        {
            foreach (var action in _inputActions)
            {
                action.CheckAction();
            }
        }
    }
}
