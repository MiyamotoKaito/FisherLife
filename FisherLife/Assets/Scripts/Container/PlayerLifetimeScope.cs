using Commons;
using PlayerModule;
using VContainer;
using VContainer.Unity;

namespace Container
{
    /// <summary>
    ///     プレイヤー関連の依存を登録するスコープ。
    /// </summary>
    public class PlayerLifetimeScope : LifetimeScope
    {
        /// <summary>
        ///     プレイヤーのView・コントローラー・状態を登録する。
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PlayerView>();
            builder.Register<PlayerController>(Lifetime.Singleton);
            builder.Register<PlayerMovePresenter>(Lifetime.Singleton);
            builder.Register<IPlayerMoveUsecase, PlayerMoveUsecase>(Lifetime.Singleton);
            builder.Register<PlayerMoveState>(Lifetime.Singleton);

            // 生成後にプレイヤー移動状態をステートマシンへ登録する。
            builder.RegisterBuildCallback(resolver =>
            {
                var machine = resolver.Resolve<IWorldStateMachine>();
                machine.AddState(resolver.Resolve<PlayerMoveState>());
            });
        }
    }
}
