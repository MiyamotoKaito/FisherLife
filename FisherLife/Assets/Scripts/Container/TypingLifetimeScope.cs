using Commons;
using InputModule;
using TypingModule;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class TypingLifetimeScope : LifetimeScope
    {
        [SerializeField] private TextAsset _textAsset;
        protected override void Configure(IContainerBuilder builder)
        {
            // View(MonoBehaviour) を ITypingView として登録（シーン階層から取得）
            builder.RegisterComponentInHierarchy<TypingView>().As<ITypingView>();

            builder.Register<TypingController>(Lifetime.Singleton);

            // 入力(ピュアクラス) を ITypingInput として登録
            builder.Register<ITypingInput, TypingInput>(Lifetime.Singleton);
            builder.RegisterInstance(_textAsset);
            // 
            builder.Register<TypingPresenter>(Lifetime.Singleton);

            builder.Register<IWordSeparatorUsecase, CSVSeparatorUsecase>(Lifetime.Singleton);

            builder.Register<TypingState>(Lifetime.Singleton);
            builder.RegisterBuildCallback(resolver =>
            {
                var machine = resolver.Resolve<IWorldStateMachine>();
                machine.AddState(resolver.Resolve<TypingState>());
            });
        }
    }
}
