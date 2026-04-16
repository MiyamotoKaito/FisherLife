using System;
using UnityEngine;

namespace Miyamoto.FisherLife.Develop.Mock.Typing
{
    public class TypingInput : MonoBehaviour
    {
        public event Action<string> OnType;
        public event Action OnEnter;
        public event Action OnBackspace;

        private void Update()
        {
            foreach (var c in UnityEngine.Input.inputString)
            {
                if (c == '\b')
                    OnBackspace?.Invoke();
                else if (c == '\n' || c == '\r')
                    OnEnter?.Invoke();
                else
                    OnType?.Invoke(c.ToString());
            }
        }
    }
}