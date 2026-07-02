namespace Commons
{
    /// <summary>
    ///     ダメージを計算した結果の構造体
    /// </summary>
    public readonly struct AttackResult
    {
        /// <summary>実際のダメージ</summary>
        public int Damage => _damage;

        public AttackResult(int damage)
        {
            _damage = damage;
        }

        private readonly int _damage;
    }
}
