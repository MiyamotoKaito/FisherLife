using R3;
using System.Threading;
using UnityEngine;

namespace PlayerModule
{
    /// <summary>
    /// プレイヤーの動きの値を反映させるPresenter
    /// </summary>
    public class PlayerMovePresenter
    {
        public PlayerMovePresenter(PlayerView playerView)
        {
            _playerView = playerView;
            _playerModel = new PlayerModel();
            _cancellationTokenSource =  new CancellationTokenSource();
            Subscribe();
        }

        public void SetMove(Vector3 dir)
        {
            _playerModel.SetDirection(dir);
        }
        private void Subscribe()
        {
            _playerModel.MoveDirection.Subscribe(_playerView.Move)
                .RegisterTo(_cancellationTokenSource.Token);
        }
        private readonly PlayerView _playerView;
        private readonly PlayerModel _playerModel;
        private readonly CancellationTokenSource _cancellationTokenSource;
    }
}
