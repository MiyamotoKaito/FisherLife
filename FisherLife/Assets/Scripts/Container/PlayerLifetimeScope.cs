using Commons;
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
            builder.Register<IPlayerMoveUsecase, PlayerMoveUsecase>(Lifetime.Singleton);
            builder.Register<PlayerMoveState>(Lifetime.Singleton);
            builder.RegisterBuildCallback(resolver =>
            {
                var machine = resolver.Resolve<IWorldStateMachine>();
                machine.AddState(resolver.Resolve<PlayerMoveState>());
            });
        }
    }
}
