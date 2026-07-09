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

            // ShoppingControllerが参照するパネル。
            builder.RegisterComponentInHierarchy<SelectTradePanel>();
            builder.RegisterComponentInHierarchy<ComfilmPanel>();

            // IController と 自身型の両方で解決できるようにする（ShoppingStateが具象を注入するため）。
            builder.Register<ShoppingController>(Lifetime.Singleton).As<IController>().AsSelf();
            builder.Register<ShoppingState>(Lifetime.Singleton).As<IState>().AsSelf();

            builder.RegisterBuildCallback(resolver =>
            {
                var machine = resolver.Resolve<IWorldStateMachine>();
                machine.AddState(resolver.Resolve<ShoppingState>());
            });
        }
    }
}
