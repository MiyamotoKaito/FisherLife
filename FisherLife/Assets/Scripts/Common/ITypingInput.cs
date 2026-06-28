using R3;
using System;

namespace Commons
{
    public interface ITypingInput : IDisposable
    {
        Observable<char> OnChar { get; }
        void SetEnable(bool enable);
    }
}
