using UnityEngine;

namespace Commons
{
    /// <summary>
    ///     釣り竿のインターフェース。攻撃手段（IAttacker）の一種。
    /// </summary>
    public interface IRod : IAttacker
    {
        public byte Level { get; }
        public Vector3 Position { get; }
    }
}
