using Commons;
using UnityEngine;
using Utility;

namespace PlayerModule
{
    /// <summary>
    ///     装備中の釣り竿。装備データ(RodData)から IRod の各値を供給する。
    ///     Equipで中身を差し替えると、釣り・戦闘(IRod参照)へ即反映される。
    /// </summary>
    public class RodModel : IRod
    {
        private RodData _equipped;
        private Vector3 _position;

        /// <summary> 装備する竿を差し替える。 </summary>
        public void Equip(RodData data) => _equipped = data;

        /// <summary> 釣り位置を設定する。 </summary>
        public void SetPosition(Vector3 position) => _position = position;

        public int Power                 => _equipped?.AttackPower ?? 0;
        public float CriticalMultiplier  => _equipped?.CriticalMultiplier ?? 1f;
        public float CriticalProbability => _equipped?.CriticalRate ?? 0f; // Rate→Probability対応
        public byte Level                => _equipped?.RodLevel ?? 1;
        public Vector3 Position          => _position;
    }
}
