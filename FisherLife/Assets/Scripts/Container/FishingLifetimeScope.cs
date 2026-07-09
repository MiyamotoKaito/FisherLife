using Commons;
using FishingModule;
using FishModule;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Container
{
    /// <summary>
    ///     釣り関連の依存を登録するスコープ。
    /// </summary>
    public class FishingLifetimeScope : LifetimeScope
    {

        /// <summary>
        ///     釣り関連の依存を登録する。
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IFishingController,FishingController>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<FishFactory>().As<IFishFactory>();
            builder.RegisterComponentInHierarchy<CatchResultPanel>();
            builder.Register<FishingState>(Lifetime.Singleton);
            builder.RegisterBuildCallback(resolver =>
            {
                var machine = resolver.Resolve<IWorldStateMachine>();
                machine.AddState(resolver.Resolve<FishingState>());
            });
        }
    }
}
