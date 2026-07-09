using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Commons
{
    public interface IFishFactory
    {
        /// <summary> 魚の生成中心（スポット）の位置。 </summary>
        Vector3 SpotPosition { get; }
        UniTask<IFish> CreateFish(IRod rod, Vector3 facing);
        void HideFish();
        /// <summary> 戦闘中のループVFXを開始する。 </summary>
        void StartBattleVfx();
        /// <summary> 戦闘中のループVFXを止める。 </summary>
        void StopBattleVfx();
    }
}
