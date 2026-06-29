using UnityEngine;

namespace PlayerModule
{
    [CreateAssetMenu(fileName = "PlayerParametor", menuName = "Scriptable Objects/PlayerParametor")]
    public class PlayerParametor : ScriptableObject
    {
        public int MoveSpeed;
    }
}
