using Common;
using InputModule;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private InputActionAsset _action;
        protected override void Configure(IContainerBuilder builder)
        {
            DontDestroyOnLoad(this.gameObject);

            builder.RegisterInstance(_action);
            builder.Register<IInputActionMapStack, InputActionMapStack>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<WorldStateMachine>();
        }
    }
}
