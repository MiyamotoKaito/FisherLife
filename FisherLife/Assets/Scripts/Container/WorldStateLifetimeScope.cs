using Common;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace WorldStateModule
{
    public class WorldStateLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<WorldStateMachine>();
        }
    }
}
