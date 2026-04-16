using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Miyamoto.FisherLife.Develop.Input
{
    public class InputActions : MonoBehaviour, IInputService
    {
        public event Action<string> OnType;
        public event Action OnEnter;
        private FisherLifeInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new FisherLifeInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Typing.KeyType.started += Type;
            _inputActions.Typing.Enter.started += Enter;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Typing.KeyType.started -= Type;
            _inputActions.Typing.Enter.started -= Enter;
        }

        private void Type(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.unityLogger.Log(context.action.name);
                var key = context.control.displayName;
                OnType?.Invoke(key);
            }
        }

        private void Enter(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnEnter?.Invoke();
            }
        }
    }
}