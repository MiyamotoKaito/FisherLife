using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

/// <summary>
/// ActionMapの切り替えを管理するクラス
/// </summary>
public class InputActionMapSwitcher : IDisposable
{
    private FisherLifeInputActions _inputActions;
    private InputActionMap _currentActionMap;
    private Stack<ActionMapType> _actionMapTypes;

    public InputActionMapSwitcher(FisherLifeInputActions inputActions)
    {
        _inputActions = inputActions;
    }

    /// <summary>
    /// 外側から呼び出すActionMapの切り替えを行うためのメソッド
    /// </summary>
    /// <param name="actionMapType"></param>
    public void ChangeMap(ActionMapType actionMapType)
    {
        switch (actionMapType)
        {
            case ActionMapType.Typing:
                MapSwitch(_inputActions.Typing);
                break;

            case ActionMapType.Fishing:
                MapSwitch(_inputActions.Fishing);
                break;
        }
    }
    /// <summary>
    /// ActionMapの切り替えを行い、切り替えたActionMapTypeをStackに積む
    /// </summary>
    /// <param name="actionMapType"></param>
    public void PushMap(ActionMapType actionMapType)
    {
        _actionMapTypes.Push(actionMapType);
    }
    /// <summary>
    /// ActionMap切り替えを行う
    /// </summary>
    /// <param name="actionMap"></param>
    private void MapSwitch(InputActionMap actionMap)
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

public enum ActionMapType
{
    Typing = 0,
    Fishing = 1,
    Player = 2,
    UI = 3,
}
