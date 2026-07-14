using Commons;
using PlayerModule;
using ShopModule;
using UnityEngine;
using Utility;
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

            // SellPanelはIFishCatalogを注入されるため登録する。
            builder.RegisterComponentInHierarchy<SellPanel>();

            // IController と 自身型の両方で解決できるようにする（ShoppingStateが具象を注入するため）。
            builder.Register<ShoppingController>(Lifetime.Singleton).As<IController>().AsSelf();
            builder.Register<ShoppingState>(Lifetime.Singleton).As<IState>().AsSelf();

            builder.RegisterBuildCallback(resolver =>
            {
                var machine = resolver.Resolve<IWorldStateMachine>();
                machine.AddState(resolver.Resolve<ShoppingState>());
            });
        }

        private void Start()
        {
            var num = Random.Range(0, 2);
            switch (num)
            {
                case 0:
                    AudioManager.Instance.PlayBGM("Love");
                    break;

                case 1:
                    AudioManager.Instance.PlayBGM("Wide");
                    break;

                case 2:
                    AudioManager.Instance.PlayBGM("Meow");
                    break;

                default:
                    AudioManager.Instance.StopBGM();
                    break;
            }
        }
    }
}
