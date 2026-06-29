using R3;
using UnityEngine;

namespace PlayerModule
{
    /// <summary>
    ///  プレイヤーのモデル
    /// </summary>
    public class PlayerModel
    {
        public PlayerModel()
        {
            _dir = new();
        }
        public ReadOnlyReactiveProperty<Vector3> MoveDirection => _dir;
        private ReactiveProperty<Vector3> _dir;
        /// <summary>
        /// 入力された方向を設定する
        /// </summary>
        /// <param name="dir">入力された方向</param>
        public void SetDirection(Vector3 dir)
        {
            _dir.Value = dir;
        }
    }
}
