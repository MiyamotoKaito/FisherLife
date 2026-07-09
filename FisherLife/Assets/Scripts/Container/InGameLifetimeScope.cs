using BattleModule;
using Commons;
using FishingModule;
using FishModule;
using PlayerModule;
using StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Container
{
    /// <summary>
    ///     インゲームで共有する依存を登録するスコープ。
    /// </summary>
    public class InGameLifetimeScope : LifetimeScope
    {
        [SerializeField] private FishListAsset _fishListAsset;
        [SerializeField, Tooltip("新規時に付与する初期竿。")] private RodParameter _starterRod;
        /// <summary>
        ///     ワールドステートマシンと動作確認用コンポーネントを登録する。
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_fishListAsset);
            builder.Register<IWorldStateMachine, WorldStateMachine>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<InGameInitializer>();
            builder.RegisterComponentInHierarchy<Camera>();
            builder.Register<IAttackPipeline, AttackPipeline>(Lifetime.Singleton);
            builder.Register<IAttackCalculator, AttackCalculator>(Lifetime.Singleton);
            builder.Register<IBattleUsecase, BattleUsecase>(Lifetime.Singleton);
            builder.Register<IFishingModeRegistry, FishingModeRegistry>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<PlayerView>().As<IPlayerFishingAnimation>().AsSelf();

            // 装備中の竿(IRod)と、その所持/装備を管理するインベントリ。
            builder.Register<RodModel>(Lifetime.Singleton).As<IRod>().AsSelf();
            builder.RegisterInstance(_starterRod);
            builder.Register<RodInventoryModel>(Lifetime.Singleton);

            // FishListAssetの辞書(レベル別・名前別)を構築する。
            builder.RegisterBuildCallback(resolver =>
            {
                resolver.Resolve<FishListAsset>().InitDictionaries();
            });
        }
    }
}
