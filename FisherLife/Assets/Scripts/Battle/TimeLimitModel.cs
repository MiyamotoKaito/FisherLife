namespace BattleModule
{
    /// <summary>
    /// タイムリミットのモデル
    /// </summary>
    public class TimeLimitModel
    {
        /// <summary>制限時間</summary>
        public float TimeLimit => _timeLimit;
        /// <summary>経過時間</summary>
        public float ElapsedTime => _elapsedTime;

        /// <summary>
        /// タイピングを始める時にタイムリミットを設定する
        /// </summary>
        /// <param name="timeLimit"></param>
        public void Start(float timeLimit)
        {
            _timeLimit = timeLimit;
            _elapsedTime = 0;
        }
        /// <summary>
        /// 経過時間を更新する
        /// </summary>
        /// <param name="currentTime"></param>
        public void UpdateCurrentTime(float currentTime)
        {
            _elapsedTime = currentTime;
        }
        private float _timeLimit;
        private float _elapsedTime;
    }
}
