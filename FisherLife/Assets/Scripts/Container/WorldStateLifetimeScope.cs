using Common;
using InputModule;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace WorldStateModule
{
    public class WorldStateLifetimeScope : LifetimeScope
    {
        [SerializeField] private InputActionAsset _action;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_action);
            builder.Register<InputActionMapStack>(Lifetime.Singleton).As<IInputActionMapStack>();
            builder.RegisterComponentInHierarchy<WorldStateMachine>();
        }
    }
}
