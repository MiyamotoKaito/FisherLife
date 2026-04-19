using System;
using UnityEngine.InputSystem;

public class TypingInputHandler : ITypingInput, IDisposable
{
    public event Action<char> OnType;
    public event Action OnEnter;
    public event Action OnBackSpace;

    private Keyboard _keyboard;
    private FisherLifeInputActions _inputActions;
    public TypingInputHandler(FisherLifeInputActions inputActions)
    {
        _keyboard = Keyboard.current;
        _inputActions = inputActions;

        InputEnable();
    }

    public void Dispose()
    {
        InputDisable();
    }
    private void InputEnable()
    {
        _keyboard.onTextInput += OnTextInput;
        _inputActions.Typing.Enter.started += OnEnterInput;
        _inputActions.Typing.BackSpace.started += OnBackSpaceInput;
    }

    private void InputDisable()
    {
        _keyboard.onTextInput -= OnTextInput;
        _inputActions.Typing.Enter.started -= OnEnterInput;
        _inputActions.Typing.BackSpace.started -= OnBackSpaceInput;
    }
    /// <summary>
    /// テキスト入力イベントの処理
    /// </summary>
    /// <param name="inputChar"></param>
    private void OnTextInput(char inputChar)
    {
        if (inputChar == '\n' || inputChar == '\b') return;

        OnType?.Invoke(inputChar);
    }

    private void OnEnterInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnEnter?.Invoke();
    }

    private void OnBackSpaceInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnBackSpace?.Invoke();
    }


}
