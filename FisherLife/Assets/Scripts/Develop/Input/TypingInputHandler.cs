using System;
using UnityEngine.InputSystem;
using VContainer;

namespace Miyamoto.FisherLife.Develop.Mock.Typing
{
    public class TypingInputHandler : ITypingInput
    {
        public event Action<char> OnType;
        public event Action OnEnter;
        public event Action OnBackSpace;

        private Keyboard _keyboard;

        public TypingInputHandler()
        {
            _keyboard = Keyboard.current;

            _keyboard.onTextInput += OnTextInput;
        }

        private void OnTextInput(char c)
        {
            if (c == '\b')
                OnBackSpace?.Invoke();
            else if (c == '\n' || c == '\r')
                OnEnter?.Invoke();
            else
                OnType?.Invoke(c);
        }
    }
}