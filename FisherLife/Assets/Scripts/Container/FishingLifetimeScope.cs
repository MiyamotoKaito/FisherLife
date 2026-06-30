using VContainer;
using VContainer.Unity;

namespace Container
{
    /// <summary>
    ///     釣り関連の依存を登録するスコープ。
    /// </summary>
    public class FishingLifetimeScope : LifetimeScope
    {
        /// <summary>
        ///     釣り関連の依存を登録する。
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
        }
    }
}
