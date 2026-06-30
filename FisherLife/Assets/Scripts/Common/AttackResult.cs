namespace Commons
{
    /// <summary>
    ///     ダメージを計算した結果の構造体
    /// </summary>
    public readonly struct AttackResult
    {
        /// <summary>実際のダメージ</summary>
        public int Damage => _damage;
        /// <summary>クリティカル判定</summary>
        public bool IsCritical => _isCritical;

        public AttackResult(int damage, bool isCritical)
        {
            _damage = damage;
            _isCritical = isCritical;
        }

        private readonly int _damage;
        private readonly bool _isCritical;
    }
}
