namespace PlayerModule
{
    public class PlayerFishingPresenter
    {
        public PlayerFishingPresenter(PlayerView playerView)
        {
            _playerView = playerView;
        }

        public bool CanFishing()
        {
            return _playerView.CanFishing;
        }
        private readonly PlayerView _playerView;
    }
}
