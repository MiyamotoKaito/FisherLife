using System.Collections.Generic;
using Commons;
using UnityEngine;
using VContainer;

namespace FishModule
{
    /// <summary>
    ///     魚を生成するファクトリー。
    /// </summary>
    public class FishFactory : MonoBehaviour, IFishFactory
    {
        public IFish CreateFish(IRod rod)
        {
            Vector3 rodPos = rod.Position;

            for (int i = 0; i < _defaultFishSpawnAmount; i++)
            {
                var fish = new GameObject($"Fish_{i}");
                var fishData = _fishListAsset.FishParameters[rod.Level];
                var selectedFishParameter = fishData[Random.Range(0, fishData.Count)];
                fish.AddComponent<FishView>().Init(selectedFishParameter);

                Vector3 initPos = new Vector3(
                    rodPos.x + Mathf.Sin(Random.Range(0f, 2f * Mathf.PI)),
                    rodPos.y,
                    rodPos.z + Mathf.Cos(Random.Range(0f, 2f * Mathf.PI)));

                fish.transform.position = initPos;
                fish.transform.LookAt(initPos);
                _fishViews.Add(fish.GetComponent<FishView>().FishModel);
            }
            return _fishViews[Random.Range(0, _fishViews.Count)];
        }
        private void Awake()
        {
            _fishViews = new List<IFish>();
        }
        [Inject] private FishListAsset _fishListAsset;
        [SerializeField] private int _defaultFishSpawnAmount = 3;
        private List<IFish> _fishViews;
    }
}
