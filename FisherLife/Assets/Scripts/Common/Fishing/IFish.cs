using System;
using UnityEngine;

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
        /// <summary> 魚の画像。 </summary>
        Sprite Image { get; }
    }
}
