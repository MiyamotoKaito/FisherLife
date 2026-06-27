using System.Collections.Generic;
using Common;
using InputModule;
using TypingModule;
using VContainer;
using VContainer.Unity;

namespace Container
{
    /// <summary>
    /// タイピング機能のコンポジションルート。
    /// 具象クラスを interface に結びつけて登録する。
    /// </summary>
    public class TypingLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // View(MonoBehaviour) を ITypingView として登録（シーン階層から取得）
            builder.RegisterComponentInHierarchy<TypingView>().As<ITypingView>();

            builder.RegisterComponentInHierarchy<TypingController>();

            // 入力(ピュアクラス) を ITypingInput として登録
            builder.Register<TypingInput>(Lifetime.Singleton).As<ITypingInput>();
            
            // 
            builder.Register<TypingPresenter>(Lifetime.Singleton);

            builder.Register<WordSeparatorUsecase>(Lifetime.Singleton).As<IWordSeparatorUsecase>();
        }
    }
}
