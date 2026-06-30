using R3;
using UnityEngine;

namespace PlayerModule
{
    /// <summary>
    ///     プレイヤーの移動方向を保持するモデル。
    /// </summary>
    public class PlayerModel
    {
        /// <summary>
        ///     移動方向のリアクティブプロパティを初期化する。
        /// </summary>
        public PlayerModel()
        {
            _dir = new ReactiveProperty<Vector3>();
        }

        /// <summary> 現在の移動方向。 </summary>
        public ReadOnlyReactiveProperty<Vector3> MoveDirection => _dir;

        /// <summary>
        ///     入力された方向を設定する。
        /// </summary>
        public void SetDirection(Vector3 dir)
        {
            _dir.Value = dir;
        }

        private readonly ReactiveProperty<Vector3> _dir;
    }
}
