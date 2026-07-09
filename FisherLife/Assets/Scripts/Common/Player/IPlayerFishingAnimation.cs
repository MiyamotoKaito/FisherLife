using UnityEngine;

namespace Commons
{
    public interface IPlayerFishingAnimation
    {
        /// <summary> プレイヤーの現在の向き。 </summary>
        Vector3 FacingDirection { get; }
        void Throw();
        void Fighting();
        void Stop();
        void GetFish();
        /// <summary> 指定した位置（釣りスポット）の方へ水平に向く。 </summary>
        void FaceTo(Vector3 targetPosition);
    }
}
