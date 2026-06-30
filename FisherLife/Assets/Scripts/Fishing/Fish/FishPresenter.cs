using System.Threading;
using R3;

namespace FishModule
{
    /// <summary>
    ///     魚のモデルの変化をViewへ反映させるPresenter。
    /// </summary>
    public class FishPresenter
    {
        /// <summary>
        ///     View・Modelとキャンセルトークンを準備する。
        /// </summary>
        public FishPresenter(FishView fishView, FishModel fishModel)
        {
            _fishView = fishView;
            _fishModel = fishModel;
            _cancellationTokenSource = new CancellationTokenSource();
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
                    // TODO:死ぬ処理。
                }
            }).RegisterTo(_cancellationTokenSource.Token);
        }
    }
}
