using System;

public interface IInputService
{
    event Action<string> OnType;
    event Action OnEnter;
}
