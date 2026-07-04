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

            foreach (var fish in _fishPresenters)
            {
                fish.SetEnable(true);
            }

            for (int i = 0; i < _defaultFishSpawnAmount; i++)
            {
                var fishData = _fishListAsset.FishParameters[rod.Level];
                var selectedFishParameter = fishData[Random.Range(0, fishData.Count)];
                _fishPresenters[i].SetFishParameter(selectedFishParameter);

                Vector3 initPos = new Vector3(
                    rodPos.x + Mathf.Sin(Random.Range(0f, 2f * Mathf.PI)),
                    rodPos.y,
                    rodPos.z + Mathf.Cos(Random.Range(0f, 2f * Mathf.PI)));

                _fishPresenters[i].SetStartPosition(initPos);
            }
            var randomIndex = Random.Range(0, _fishPresenters.Count);
            return _fishPresenters[randomIndex].FishModel;
        }
        public void HideFish()
        {
            foreach (var fish in _fishPresenters)
            {
                fish.SetEnable(false);
            }
        }
        private void Awake()
        {
            _fishPresenters = new List<FishPresenter>();
            for (int i = 0; i < _defaultFishSpawnAmount; i++)
            {
                var fishView = Instantiate(_fishPrefab, Vector3.zero, Quaternion.identity);
                if (!fishView.TryGetComponent<FishView>(out var fishViewComponent))
                {
                    Debug.LogError("生成された魚のビューにFishViewコンポーネントがありません。");
                    return;
                }
                fishViewComponent.SetEnable(false);
                fishView.name = $"Fish_{i}";

                var newFishPresenter = new FishPresenter(fishViewComponent);
                _fishPresenters.Add(newFishPresenter);
            }
        }
        [Inject] private FishListAsset _fishListAsset;
        [SerializeField] private GameObject _fishPrefab;
        [SerializeField] private int _defaultFishSpawnAmount = 3;
        private List<FishPresenter> _fishPresenters;
    }
}
