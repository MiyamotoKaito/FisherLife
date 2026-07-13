using System.Collections.Generic;

namespace Commons
{
    ///<summary>全釣り竿のパラメータ一覧。</summary>
    public interface IRodCatalog
    {
        ///<summary>登録されている全釣り竿。</summary>
        IReadOnlyList<IRodParameter> Rods { get; }
    }
}
