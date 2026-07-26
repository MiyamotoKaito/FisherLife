using Commons;

namespace TitleModule
{
    /// <summary>
    /// タイトルステート
    /// </summary>
    public class TitleState : IState
    {
        public TitleState(IInputActionMapStack inputActionMapStack,TitleController titleController)
        {
            _titleController = titleController;
            _inputActionMapStack = inputActionMapStack;
        }

        public WorldStateType WorldState => WorldStateType.OutGame;

        private readonly IInputActionMapStack _inputActionMapStack;
        private readonly TitleController _titleController;
        public void Entry()
        {
            _inputActionMapStack.Push(InputActionMapType.Title);
            _titleController.Begin();
        }

        public void Exit()
        {
            _titleController.End();
        }
    }
}
