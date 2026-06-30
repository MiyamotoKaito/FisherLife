using UnityEngine;

namespace PlayerModule
{
    /// <summary>
    ///     プレイヤーのパラメータを保持するデータコンテナ。
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerParametor", menuName = "Scriptable Objects/PlayerParametor")]
    public class PlayerParametor : ScriptableObject
    {
        /// <summary> 移動速度。 </summary>
        public int MoveSpeed => _moveSpeed;

        [SerializeField, Tooltip("移動速度。")]
        private int _moveSpeed;
    }
}
