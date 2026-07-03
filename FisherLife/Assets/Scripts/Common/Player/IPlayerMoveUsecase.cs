using UnityEngine;

namespace Commons
{
    /// <summary>
    ///     プレイヤー移動に関する変換処理のユースケース。
    /// </summary>
    public interface IPlayerMoveUsecase
    {
        /// <summary>
        ///     入力されたVector2を移動方向のVector3へ変換する。
        /// </summary>
        Vector3 ToVector3(Vector2 readValue);
    }
}
