using UnityEngine;

namespace Commons
{
    public interface IInteractable
    {
        string DisplayDescription { get; }
        void Interact();
    }
}
