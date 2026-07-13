using UnityEngine;

namespace Commons
{
    ///<summary>魚のパラメータ（表示・売買で使う読み取り用）。</summary>
    public interface IFishParameter
    {
        ///<summary>名前。</summary>
        string Name { get; }
        ///<summary>入手できる金額。</summary>
        int SellingPrice { get; }
        ///<summary>魚の画像。</summary>
        Sprite Image { get; }
    }
}
