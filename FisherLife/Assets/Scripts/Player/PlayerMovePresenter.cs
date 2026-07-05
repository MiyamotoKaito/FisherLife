using R3;
using System;
using System.Threading;
using UnityEngine;

namespace PlayerModule
{
    /// <summary>
    ///     プレイヤーの移動量をViewへ反映させるPresenter。
    /// </summary>
    public class PlayerMovePresenter : IDisposable
    {
        /// <summary>
        ///     View・Modelを準備し、購読を開始する。
        /// </summary>
        public PlayerMovePresenter(PlayerView playerView)
        {
            _playerView = playerView;
            _playerModel = new PlayerModel();
            _cancellationTokenSource = new CancellationTokenSource();
            Subscribe();
        }

        /// <summary>
        ///     移動方向を設定する。
        /// </summary>
        public void SetMove(Vector3 dir)
        {
            _playerModel.SetDirection(dir);
        }

        private readonly PlayerView _playerView;
        private readonly PlayerModel _playerModel;
        private readonly CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///     モデルの移動方向をViewへ橋渡しする購読を登録する。
        /// </summary>
        private void Subscribe()
        {
            _playerModel.MoveDirection.Subscribe(_playerView.Move)
                .RegisterTo(_cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
            if (_playerModel.MoveDirection != null)
            {
                _playerModel.MoveDirection.Dispose();
            }
        }
    }
}
