using System;
using UnityEngine.InputSystem;

/// <summary>
/// ActionMapの切り替えを管理するクラス
/// </summary>
public class InputActionMapSwitcher : IDisposable
{
    private InputActionMap _currentActionMap;

    /// <summary>
    /// ActionMap切り替えを行う
    /// </summary>
    /// <param name="actionMap"></param>
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
