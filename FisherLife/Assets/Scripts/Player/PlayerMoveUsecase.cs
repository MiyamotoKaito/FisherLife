using Commons;
using UnityEngine;

namespace PlayerModule
{
    /// <summary>
    ///     プレイヤー移動に関する変換処理のユースケース。
    /// </summary>
    public class PlayerMoveUsecase : IPlayerMoveUsecase
    {
        /// <summary>
        ///     InputSystemから渡されたVector2を移動方向のVector3へ変換する。
        /// </summary>
        public Vector3 ToVector3(Vector2 readValue)
        {
            return new Vector3(readValue.x, 0, readValue.y);
        }
    }
}
