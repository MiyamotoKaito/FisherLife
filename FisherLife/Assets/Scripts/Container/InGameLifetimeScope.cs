using BattleModule;
using Commons;
using FishingModule;
using PlayerModule;
using StateMachine;
using Utility;
using VContainer;
using VContainer.Unity;

namespace Container
{
    /// <summary>
    ///     インゲームで共有する依存を登録するスコープ。
    /// </summary>
    public class InGameLifetimeScope : LifetimeScope
    {
        /// <summary>
        ///     ワールドステートマシンと動作確認用コンポーネントを登録する。
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IWorldStateMachine, WorldStateMachine>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<InGameInitializer>();
            builder.Register<IAttackPipeline, AttackPipeline>(Lifetime.Singleton);
            builder.Register<IAttackCalculator, AttackCalculator>(Lifetime.Singleton);
            builder.Register<IBattleUsecase, BattleUsecase>(Lifetime.Singleton);
            builder.Register<IFishingModeRegistry, FishingModeRegistry>(Lifetime.Singleton);
            builder.Register<IRod, RodModel>(Lifetime.Singleton);
        }
    }
}
