using UnityEngine;
using VContainer;

namespace BattleModule
{
    /// <summary>
    /// タイムリミットのPresenter
    /// </summary>
    public class TimeLimitPresenter : MonoBehaviour
    {
        [Inject]
        public void Init(TimeLimitView timeLimitView,
            TimeLimitModel timeLimitModel)
        {
            _timeLimitView = timeLimitView;
            _timeLimitModel = timeLimitModel;
        }
        private TimeLimitView _timeLimitView;
        private TimeLimitModel _timeLimitModel;
        private bool _active = false;
        /// <summary>
        /// 制限時間を設定する
        /// </summary>
        /// <param name="timeLimit"></param>
        public void SetLimit(float timeLimit)
        {
            Debug.Log("時間を設定");
            _timeLimitModel.Start(timeLimit);
            _active = true;
        }
        /// <summary>
        /// Viewの表示を更新する
        /// </summary>
        public void UpdateView()
        {
            float elapsedTime = _timeLimitModel.ElapsedTime / _timeLimitModel.TimeLimit;

            _timeLimitView.UpdateImage(1 - elapsedTime);
        }
        /// <summary>
        /// 戦闘が終わった時に呼ぶメソッド
        /// </summary>
        public void End()
        {
            _active = false;
        }
        private void Update()
        {
            if (_active)
            {
                UpdateView();
                _timeLimitModel.UpdateCurrentTime(Time.deltaTime);
            }
        }
    }
}
