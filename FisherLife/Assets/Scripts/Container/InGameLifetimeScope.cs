using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class InGameLifetimeScope : LifetimeScope
    {
        [SerializeField] private InputActionAsset _actionAsset;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_actionAsset);
        }
    }
}