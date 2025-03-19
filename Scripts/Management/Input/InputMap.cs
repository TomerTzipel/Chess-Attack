
namespace ChessOut.Input
{
    //Encapsulates a List of InputAction, allows to add an action and to check all actions for activation.
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
                action.CheckIfActionTriggered();
            }
        }
    }
}
