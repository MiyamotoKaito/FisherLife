using Commons;
using R3;
using UnityEngine;

namespace FishModule
{
    /// <summary>
    ///     魚の状態を保持するモデル。
    /// </summary>
    public class FishModel : IFish
    {
        public FishModel()
        {
            _hp = new ReactiveProperty<int>(0);
        }
        /// <summary>
        ///     パラメータから初期体力を設定する。
        /// </summary>
        public void SetParameter(FishParameter fishParamater)
        {
            _paramater = fishParamater;
            _hp.Value = fishParamater.Hp;
        }

        /// <summary> 体力。 </summary>
        public ReactiveProperty<int> Hp => _hp;
        /// <summary> 防御力。 </summary>
        public int Defence => _paramater.Defence;
        /// <summary> 名前。 </summary>
        public string Name => _paramater.Name;
        /// <summary> 入手できる金額。 </summary>
        public int SellingPrice => _paramater.SellingPrice;
        /// <summary> レベル。 </summary>
        public int Level => _paramater.Level;
        /// <summary> 魚の画像。 </summary>
        public Sprite Image => _paramater.Image;

        /// <summary>
        ///     ダメージを受け、体力を減らす。
        /// </summary>
        public void TakeDamage(int damage)
        {
            _hp.Value = Mathf.Max(0, _hp.Value - damage);
        }

        public void Dispose()
        {
            if (_hp != null)
            {
                _hp.Dispose();
            }
        }

        private ReactiveProperty<int> _hp;
        private FishParameter _paramater;
    }
}
