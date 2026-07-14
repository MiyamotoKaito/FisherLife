using UnityEngine;

namespace PlayerModule
{
    /// <summary>
    /// プレイヤーのインタラクションを管理するクラス。
    /// </summary>
    public class PlayerInteractor
    {
        public PlayerInteractor(PlayerView playerView)
        {
            _playerView = playerView;
        }
        /// <summary>
        ///    プレイヤーがインタラクト可能なオブジェクトと接触している場合、インタラクトを実行する。
        /// </summary>
        public void Interact()
        {
            Debug.Log($"[Interact] Interactable = {_playerView.Interactable}");
            if (_playerView.Interactable != null)
            {
                _playerView.Interactable.Interact();
            }
        }
        private readonly PlayerView _playerView;
    }
}
