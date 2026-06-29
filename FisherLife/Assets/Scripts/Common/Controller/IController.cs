using System;

namespace Commons
{
    public interface IController : IDisposable
    {
        InputActionMapType InputActionMapType { get; }
        void Enable();
        void Disable();
    }
}
