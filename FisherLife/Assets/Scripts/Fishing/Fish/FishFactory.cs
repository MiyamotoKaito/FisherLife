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
                var level = SelectSpawnLevel(rod.Level);
                var fishData = _fishListAsset.FishParameters[level];
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

        /// <summary>
        ///     スポーンする魚のレベルを決める。
        ///     90%で現在レベル、10%で一つ上のレベル。ただし一つ上が辞書に無ければ現在レベル。
        /// </summary>
        private byte SelectSpawnLevel(byte rodLevel)
        {
            if (Random.value < HIGHER_LEVEL_RATE)
            {
                byte higher = (byte)(rodLevel + 1);
                if (_fishListAsset.FishParameters.ContainsKey(higher))
                {
                    return higher;
                }
            }
            return rodLevel;
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
        private const float HIGHER_LEVEL_RATE = 0.1f; // 一つ上のレベルの魚がスポーンする確率(10%)
        [Inject] private FishListAsset _fishListAsset;
        [SerializeField] private GameObject _fishPrefab;
        [SerializeField] private int _defaultFishSpawnAmount = 3;
        private List<FishPresenter> _fishPresenters;
    }
}
