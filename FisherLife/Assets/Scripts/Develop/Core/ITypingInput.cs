using System;

public interface ITypingInput
{
    event Action<char> OnType;
    event Action OnEnter;
    event Action OnBackSpace;
}
