using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Commons
{
    /// <summary>
    ///  インストーローラの基底クラス。
    /// </summary>
    public abstract class InstrallerObject : ScriptableObject, IInstaller
    {
        public abstract void Install(IContainerBuilder builder);
    }
}
