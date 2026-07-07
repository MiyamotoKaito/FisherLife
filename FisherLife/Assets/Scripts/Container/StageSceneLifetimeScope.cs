using PlayerModule;
using Utility;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class StageSceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<Door>();
            builder.RegisterComponentInHierarchy<FishingArea>();
            builder.RegisterComponentInHierarchy<FishingPriorityChange>();
        }
    }
}
