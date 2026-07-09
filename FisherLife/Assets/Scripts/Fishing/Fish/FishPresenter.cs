using System;
using System.Threading;
using Commons;
using R3;
using UnityEngine;

namespace FishModule
{
    /// <summary>
    ///     魚のモデルの変化をViewへ反映させるPresenter。
    /// </summary>
    public class FishPresenter : IDisposable
    {
        /// <summary>
        ///     View・Modelとキャンセルトークンを準備する。
        /// </summary>
        public FishPresenter(FishView fishView)
        {
            _fishView = fishView;
            _fishModel = new FishModel();
            _cancellationTokenSource = new CancellationTokenSource();

            Subscribe();
        }
        public IFish FishModel => _fishModel;
        public void SetStartPosition(Vector3 pos)
        {
            _fishView.SetStartPosition(pos);
        }
        public void SetEnable(bool enable)
        {
            _fishView.SetEnable(enable);
        }
        public void SetRotate(Vector3 pos)
        {
            _fishView.Look(pos);
        }
        public void SetFishParameter(FishParameter fishParameter)
        {
            _fishModel.SetParameter(fishParameter);
        }
        public void Dispose()
        {
            if(_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }
        private readonly FishView _fishView;
        private readonly FishModel _fishModel;
        private readonly CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///     体力の変化を購読する。
        /// </summary>
        private void Subscribe()
        {
            _fishModel.Hp.Subscribe(hp =>
            {
                if (hp <= 0)
                {
                    // 死ぬ処理。
                    _cancellationTokenSource?.Cancel();
                    _fishView.SetEnable(false);
                }
            }).RegisterTo(_cancellationTokenSource.Token);
        }

    }
}
