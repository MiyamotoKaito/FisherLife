using Commons;
using InputModule;
using PlayerModule;
using StateMachine;
using TypingModule;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class InGameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<WorldStateMachine>().As<IWorldStateMachine>();
            #region PlayerMoveの登録
            builder.RegisterComponentInHierarchy<PlayerView>();
            #endregion

            #region Typingの登録
            // View(MonoBehaviour) を ITypingView として登録（シーン階層から取得）
            builder.RegisterComponentInHierarchy<TypingView>().As<ITypingView>();

            builder.RegisterComponentInHierarchy<TypingController>();

            // 入力(ピュアクラス) を ITypingInput として登録
            builder.Register<ITypingInput, TypingInput>(Lifetime.Singleton);

            // 
            builder.Register<TypingPresenter>(Lifetime.Singleton);

            builder.Register<IWordSeparatorUsecase, CSVSeparatorUsecase>(Lifetime.Singleton);
            #endregion
        }
    }
}