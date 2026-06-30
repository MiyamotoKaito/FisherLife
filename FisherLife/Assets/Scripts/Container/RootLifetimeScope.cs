using Commons;
using InputModule;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Container
{
    /// <summary>
    ///     アプリ全体で共有する依存を登録するルートスコープ。
    /// </summary>
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField, Tooltip("入力定義のアクションアセット。")]
        private InputActionAsset _action;

        /// <summary>
        ///     入力アセットとアクションマップスタックを登録する。
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
            // シーンをまたいで保持する。
            DontDestroyOnLoad(this.gameObject);

            builder.RegisterInstance(_action);
            builder.Register<IInputActionMapStack, InputActionMapStack>(Lifetime.Singleton);
        }
    }
}
