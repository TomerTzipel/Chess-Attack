using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public abstract class InputAction
    {
        public event EventHandler Action;
        protected EventArgs _eventArgs;

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
