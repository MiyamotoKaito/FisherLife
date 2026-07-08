using Commons;
using PlayerModule;
using ShopModule;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class ShoppingLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MoneyModel>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<MoneyPresenter>();
            builder.RegisterComponentInHierarchy<MoneyView>();
            builder.Register<IController, ShoppingController>(Lifetime.Singleton);
            builder.Register<ShoppingState>(Lifetime.Singleton).As<IState>().AsSelf();

            builder.RegisterBuildCallback(resolver =>
            {
                var machine = resolver.Resolve<IWorldStateMachine>();
                machine.AddState(resolver.Resolve<ShoppingState>());
            });
        }
    }
}
