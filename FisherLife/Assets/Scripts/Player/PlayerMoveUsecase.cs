using Commons;
using UnityEngine;

namespace PlayerModule
{
    public class PlayerMoveUsecase : IPlayerMoveUsecase
    {
        /// <summary>
        /// InputSystemの渡されたVector2をVector3に変換する
        /// </summary>
        /// <param name="readValue"></param>
        /// <returns></returns>
        public Vector3 ToVector3(Vector2 readValue)
        {
            return new Vector3(readValue.x, 0, readValue.y); 
        }
    }
}