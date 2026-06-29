using PlayerModule;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class PlayerLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PlayerView>();
            builder.Register<PlayerController>(Lifetime.Singleton);
            builder.Register<PlayerMovePresenter>(Lifetime.Singleton);
        }
    }
}
