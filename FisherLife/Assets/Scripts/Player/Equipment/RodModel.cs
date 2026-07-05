using Commons;
using UnityEngine;

namespace PlayerModule
{
    public class RodModel : IRod
    {
        public RodModel()
        {

        }

        public int Power => 5;

        public float CriticalMultiplier => 2;

        public float CriticalProbability => 50;

        public byte Level => 1;

        public Vector3 Position => new(0, 0, 0);
        public void SetPosition(Vector3 position)
        {
            _position = position;
        }
        private Vector3 _position;
    }
}
