using Commons;
using R3;

namespace FishModule
{
    /// <summary>
    ///     魚の状態を保持するモデル。
    /// </summary>
    public class FishModel : IFish
    {
        /// <summary>
        ///     パラメータから初期体力を設定する。
        /// </summary>
        public FishModel(FishParameter fishParamater)
        {
            _paramater = fishParamater;
            _hp = new(fishParamater.Hp);
        }

        /// <summary> 体力。 </summary>
        public ReactiveProperty<int> Hp => _hp;
        /// <summary> 防御力。 </summary>
        public int Defence => _paramater.Defence;
        /// <summary> 名前。 </summary>
        public string Name => _paramater.Name;
        /// <summary> 入手できる金額。 </summary>
        public int MoneyAmount => _paramater.MoneyAmount;
        /// <summary> レベル。 </summary>
        public int Level => _paramater.Level;

        /// <summary>
        ///     ダメージを受け、体力を減らす。
        /// </summary>
        public void TakeDamage(int damage)
        {
            _hp.Value -= damage;
        }

        private readonly ReactiveProperty<int> _hp;
        private readonly FishParameter _paramater;
    }
}
