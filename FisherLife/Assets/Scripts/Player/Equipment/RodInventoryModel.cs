using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Utility;

namespace PlayerModule
{
    /// <summary>
    ///     所持している釣り竿と装備中の竿を管理する。
    ///     装備するとIRod(RodModel)へ反映される。
    /// </summary>
    public class RodInventoryModel
    {
        private readonly RodModel _rodModel;
        private readonly RodParameter _starterRod;
        private RodCountData _data;

        public RodInventoryModel(RodModel rodModel, RodParameter starterRod)
        {
            _rodModel = rodModel;
            _starterRod = starterRod;
        }

        /// <summary> 所持している竿。 </summary>
        public IReadOnlyList<RodData> Rods => _data.RodList;
        /// <summary> 装備中の竿。 </summary>
        public RodData Equipped { get; private set; }

        public async UniTask InitializeAsync()
        {
            _data = await SaveSystem.LoadAsync<RodCountData>();

            // 所持が空ならスターター竿を付与する。
            if (_data.RodList.Count == 0)
            {
                _data.RodList.Add(CreateStarterRod());
                await SaveSystem.SaveAsync<RodCountData>();
            }

            // 装備中を復元（記録が無ければ先頭を装備）。
            var equipped = _data.RodList.Find(r => r.RodName == _data.EquippedRodName)
                           ?? _data.RodList[0];
            Equip(equipped);
        }

        /// <summary> 竿を装備する（IRodへ反映）。永続化は SaveAsync で。 </summary>
        public void Equip(RodData rod)
        {
            Equipped = rod;
            _data.EquippedRodName = rod.RodName;
            _rodModel.Equip(rod);
        }

        public UniTask SaveAsync() => SaveSystem.SaveAsync<RodCountData>();

        private RodData CreateStarterRod() => new RodData
        {
            RodName = _starterRod.Name,
            RodLevel = _starterRod.Level,
            AttackPower = _starterRod.AttackPower,
            CriticalMultiplier = _starterRod.CriticalMutiplier,
            CriticalRate = _starterRod.CriticalRate,
        };
    }
}
