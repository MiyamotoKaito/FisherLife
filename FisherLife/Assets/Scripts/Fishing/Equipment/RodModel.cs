using Commons;
using UnityEngine;

namespace FishingModule
{
    public class RodModel : IRod
    {
        public RodModel()
        {

        }

        public int Power => 5;

        public float CriticalMultiplier => 2;

        public float CriticalProbability => 50;
    }
}
