using UnityEngine;

namespace PlayerModule
{
    public class PlayerSpawner : MonoBehaviour
    {

        private class PlayerSpawnPoint
        {
            public Transform Position => _position;
            private Transform _position;
        }
    }
}
