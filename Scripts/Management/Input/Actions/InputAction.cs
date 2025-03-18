using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOut.Input
{
    public abstract class InputAction
    {
        protected EventArgs _eventArgs;

        public event EventHandler Action;

        public InputAction(EventArgs eventArgs)
        {
            _eventArgs = eventArgs;
        }

        public abstract void CheckAction();

        protected void ExecuteAction(EventArgs eventArgs)
        {
            Action?.Invoke(this, eventArgs);
        }
    }
}
