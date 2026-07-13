using System.Collections.Generic;

namespace Commons
{
    ///<summary>全魚種のパラメータ一覧。</summary>
    public interface IFishCatalog
    {
        ///<summary>登録されている全魚種。</summary>
        IReadOnlyList<IFishParameter> Fishes { get; }
    }
}
