using Commons;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace TitleModule
{
    /// <summary>
    /// タイトル管理クラス
    /// </summary>
    public class TitleController : IController
    {
        public TitleController(
            InputActionAsset inputActions,
            IWorldStateMachine worldStateMachine)
        {
            _inputAsset = inputActions;
            _worldStateMachine = worldStateMachine;

            _actionMap = _inputAsset.FindActionMap("Title");
            _entryAction = _actionMap.FindAction("Entry");
        }
        public InputActionMapType InputActionMapType => InputActionMapType.Title;
        private readonly InputActionMap _actionMap;
        private readonly InputAction _entryAction;
        private readonly InputActionAsset _inputAsset;
        private readonly IWorldStateMachine _worldStateMachine;
        public void Begin()
        {
            Debug.Log("za");
            _entryAction.started += Entry;
            Debug.Log("za");
            AudioManager.Instance.PlayBGM("Title");
        }

        public void Dispose()
        {
            _entryAction.started -= Entry;
        }

        public void End()
        {
            _entryAction.started -= Entry;
            AudioManager.Instance.StopBGM();
        }
        /// <summary>
        /// インゲームに遷移する
        /// </summary>
        /// <param name="callbackContext"></param>
        private void Entry(InputAction.CallbackContext callbackContext)
        {
            AudioManager.Instance.StopBGM();
            _worldStateMachine.AllStop();
            SceneTransitionManager.IrisIn("InGame").Forget();
        }
    }
}
