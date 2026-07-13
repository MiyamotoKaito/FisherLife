using UnityEngine;

namespace Commons
{
    ///<summary>釣り竿のパラメータ（表示・売買で使う読み取り用）。</summary>
    public interface IRodParameter
    {
        ///<summary>名前。</summary>
        string Name { get; }
        ///<summary>買値。</summary>
        int Price { get; }
        ///<summary>レベル。</summary>
        byte Level { get; }
        ///<summary>攻撃力。</summary>
        int AttackPower { get; }
        ///<summary>クリティカル倍率。</summary>
        float CriticalMutiplier { get; }
        ///<summary>クリティカル発生倍率。</summary>
        float CriticalRate { get; }
        ///<summary>画像。</summary>
        Sprite Image { get; }
        ///<summary>説明文。</summary>
        string Description { get; }
    }
}
