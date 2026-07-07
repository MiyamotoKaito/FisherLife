using Commons;
using R3;
using VContainer;

namespace Utility
{
    /// <summary>
    ///     釣りが始まった時のCinemachineの優先度を設定する
    /// </summary>
    public class FishingPriorityChange : CinemachineCameraPrioriyChanger
    {
        private void Start()
        {
            _worldStateMachine.CurrentStateType.Subscribe(state =>
            {
                if (state == WorldStateType.Fishing || state == WorldStateType.Typing)
                {
                    _cinemachineCamera.Priority = _priority;
                    _cinemachineCamera.gameObject.SetActive(true);
                }
                else
                {
                    _cinemachineCamera.Priority = CINEMACHINE_CAMERA_PRIORITY;
                    _cinemachineCamera.gameObject.SetActive(false);
                }
            }).RegisterTo(this.destroyCancellationToken);
        }

        [Inject] private IWorldStateMachine _worldStateMachine;
    }
}
