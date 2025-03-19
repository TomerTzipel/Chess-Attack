

namespace ChessOut.Input
{
    //An abstract class for triggering event from user input
    //Uses EventHandler with EventArgs to allow for creating different args with inheritance of EventArgs
    public abstract class InputAction
    {
        protected EventArgs _eventArgs;

        public event EventHandler Action;

        public InputAction(EventArgs eventArgs)
        {
            _eventArgs = eventArgs;
        }

        //Children define what they check for activation,
        
        public abstract void CheckIfActionTriggered();

        protected void ExecuteAction(EventArgs eventArgs)
        {
            Action?.Invoke(this, eventArgs);
        }
    }
}
