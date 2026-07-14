using System.Collections.Generic;
using Commons;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace FishModule
{
    /// <summary>
    ///     魚を生成するファクトリー。
    /// </summary>
    public class FishFactory : MonoBehaviour, IFishFactory
    {
        public Vector3 SpotPosition => _spot.Position;

        public async UniTask<IFish> CreateFish(IRod rod, Vector3 facing)
        {
            // 投げてから着水するまで少し待つ。
            await UniTask.Delay(1000);
            AudioManager.Instance.PlaySE("WaterDrop");
            // スポットを投げた方向へ向け、水しぶきを一回出す。
            _spot.AimTo(facing);
            _spot.PlaySplash();

            Vector3 spotPos = _spot.Position;
            await UniTask.Delay(1000);
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

                // スポットを中心に Radius ぶん離して円状に配置する。
                float angle = Random.Range(0f, 2f * Mathf.PI);
                Vector3 initPos = new Vector3(
                    spotPos.x + Mathf.Sin(angle) * _spawnRadius,
                    spotPos.y,
                    spotPos.z + Mathf.Cos(angle) * _spawnRadius);

                _fishPresenters[i].SetStartPosition(initPos);
                // 位置を決めてからスポットの方を向かせる。
                _fishPresenters[i].SetRotate(spotPos);
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

        public void StartBattleVfx() => _spot.StartBattleVfx();
        public void StopBattleVfx() => _spot.StopBattleVfx();

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
        [Inject] private FishingSpot _spot;
        [SerializeField] private GameObject _fishPrefab;
        [SerializeField] private int _defaultFishSpawnAmount = 3;
        [SerializeField, Tooltip("スポットからの生成半径。")] private float _spawnRadius = 3f;
        private List<FishPresenter> _fishPresenters;
    }
}
