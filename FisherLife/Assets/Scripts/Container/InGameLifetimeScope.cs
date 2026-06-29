using Commons;
using InputModule;
using PlayerModule;
using StateMachine;
using TypingModule;
using UnityEngine;
using Utility;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class InGameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IWorldStateMachine, WorldStateMachine>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<ModuleTest>();
        }
    }
}