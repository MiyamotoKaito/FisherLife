using Commons;
using InputModule;
using TypingModule;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Container
{
    /// <summary>
    ///     タイピング関連の依存を登録するスコープ。
    /// </summary>
    public class TypingLifetimeScope : LifetimeScope
    {
        [SerializeField, Tooltip("出題に使う単語のCSVテキスト。")]
        private TextAsset _textAsset;

        /// <summary>
        ///     タイピングのView・入力・状態などを登録する。
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
            // View(MonoBehaviour)をITypingViewとしてシーン階層から登録する。
            builder.RegisterComponentInHierarchy<TypingView>().As<ITypingView>();

            builder.Register<TypingController>(Lifetime.Singleton);

            // 入力（ピュアクラス）をITypingInputとして登録する。
            builder.Register<ITypingInput, TypingInput>(Lifetime.Singleton);
            builder.RegisterInstance(_textAsset);
            builder.Register<TypingPresenter>(Lifetime.Singleton);
            builder.Register<IWordSeparatorUsecase, CSVSeparatorUsecase>(Lifetime.Singleton);

            builder.Register<TypingState>(Lifetime.Singleton);

            // 生成後にタイピング状態をステートマシンへ登録する。
            builder.RegisterBuildCallback(resolver =>
            {
                var machine = resolver.Resolve<IWorldStateMachine>();
                machine.AddState(resolver.Resolve<TypingState>());
            });
        }
    }
}
