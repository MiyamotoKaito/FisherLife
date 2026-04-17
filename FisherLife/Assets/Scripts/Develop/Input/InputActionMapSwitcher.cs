using System;
using UnityEngine.InputSystem;

public class InputActionMapSwitcher : IDisposable
{
    private FisherLifeInputActions _inputActions;
    private InputActionMap _currentActionMap;
    public void ChangeContext(InputActionMap actionMap)
    {
        // 初回のアクションマップ設定
        if (_currentActionMap == null)
        {
            _currentActionMap = actionMap;
            _currentActionMap.Enable();
        }

        if (_currentActionMap != actionMap)
        {
            _currentActionMap?.Disable();

            _currentActionMap = actionMap;
            _currentActionMap.Enable();
        }
    }

    public void Dispose()
    {
        _currentActionMap?.Disable();
        _currentActionMap = null;
    }
}
