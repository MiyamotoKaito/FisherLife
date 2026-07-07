using System;

namespace Commons
{
    /// <summary>
    ///     魚のインターフェース。
    /// </summary>
    public interface IFish : IDamageable, IDisposable
    {
        /// <summary> 名前。 </summary>
        string Name { get; }
        /// <summary> 入手できる金額。 </summary>
        int SellingPrice { get; }
        /// <summary> レベル。 </summary>
        int Level { get; }
    }
}
