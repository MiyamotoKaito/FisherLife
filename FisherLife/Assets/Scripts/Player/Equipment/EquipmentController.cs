using Commons;
using UnityEngine.InputSystem;

namespace PlayerModule
{
    /// <summary>
    ///     装備画面の入力を受け取り、パネル操作と退出を制御する。
    /// </summary>
    public class EquipmentController : IController
    {
        public EquipmentController(InputActionAsset inputActions,
            IWorldStateMachine worldStateMachine,
            EquipmentPanel panel)
        {
            _worldStateMachine = worldStateMachine;
            _panel = panel;

            _map = inputActions.FindActionMap(InputActionMapType.Equipment.ToString(), true);
            _upAction = _map.FindAction("Up");
            _downAction = _map.FindAction("Down");
            _entryAction = _map.FindAction("Entry");
            _cancelAction = _map.FindAction("Cancel"); // 未定義なら null
        }

        public InputActionMapType InputActionMapType => InputActionMapType.Equipment;

        public void Begin()
        {
            _upAction.started += Up;
            _downAction.started += Down;
            _entryAction.started += Entry;
            if (_cancelAction != null) _cancelAction.started += Cancel;

            _panel.gameObject.SetActive(true);
            _panel.Begin();
        }

        public void End()
        {
            _upAction.started -= Up;
            _downAction.started -= Down;
            _entryAction.started -= Entry;
            if (_cancelAction != null) _cancelAction.started -= Cancel;

            // 破棄時(Play停止/シーン破棄)は panel が先に破棄されていることがあるためガードする。
            if (_panel != null) _panel.gameObject.SetActive(false);
        }

        public void Dispose() => End();

        private void Up(InputAction.CallbackContext c) => _panel.Up();
        private void Down(InputAction.CallbackContext c) => _panel.Down();
        private void Entry(InputAction.CallbackContext c) => _panel.Entry();
        private void Cancel(InputAction.CallbackContext c) => _worldStateMachine.BackState();

        private readonly IWorldStateMachine _worldStateMachine;
        private readonly EquipmentPanel _panel;
        private readonly InputActionMap _map;
        private readonly InputAction _upAction;
        private readonly InputAction _downAction;
        private readonly InputAction _entryAction;
        private readonly InputAction _cancelAction;
    }
}
