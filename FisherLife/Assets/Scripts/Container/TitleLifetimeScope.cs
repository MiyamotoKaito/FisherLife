using Commons;
using StateMachine;
using TitleModule;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Container
{
    /// <summary>
    /// タイトルのLifetimeScope
    /// </summary>
    public class TitleLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TitleState>(Lifetime.Singleton);
            builder.Register<TitleController>(Lifetime.Singleton);
            builder.Register<IWorldStateMachine, WorldStateMachine>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<OutGameInitializer>();
            builder.RegisterBuildCallback(resolver =>
            {
                var machine = resolver.Resolve<IWorldStateMachine>();
                machine.AddState(resolver.Resolve<TitleState>());
            });
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
