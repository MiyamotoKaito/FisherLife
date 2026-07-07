using Commons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerModule
{
    /// <summary>
    ///     プレイヤーの見た目と物理挙動を扱うView。
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        public IInteractable Interactable => _currentInteractableObject != null ? _currentInteractableObject : null;

        /// <summary>
        ///     移動方向を設定する。
        /// </summary>
        public void Move(Vector3 dir)
        {
            if (_animator == null) return;

            _direction = dir;
            if (_direction.sqrMagnitude > 0.01)
            {
                _animator.SetBool(MOVE_ANIMATION, true);
            }
            else
            {
                _animator.SetBool(MOVE_ANIMATION, false);
            }
        }
        /// <summary>
        ///     釣り竿を投げる
        /// </summary>
        public void Throw()
        {
            _animator.SetTrigger(THROW_ANIMATION);
        }
        /// <summary>
        ///     戦闘開始
        /// </summary>
        public void Fighting()
        {
            _animator.SetTrigger(FISHON_ANIMATION);
        }
        /// <summary>
        ///     釣りを終了して戻る。
        /// </summary>
        public void Stop()
        {
            _animator.SetTrigger(FAILED_ANIMATION);
        }

        /// <summary>
        ///     魚を入手する。
        /// </summary>
        public void GetFish()
        {
            _animator.SetTrigger(CAUGHT_ANIMATION);
        }
        private const string MOVE_ANIMATION = "Move";
        private const string THROW_ANIMATION = "Throw";
        private const string FAILED_ANIMATION = "Failed";
        private const string FISHON_ANIMATION = "FishOn";
        private const string CAUGHT_ANIMATION = "Caught";

        [SerializeField, Tooltip("プレイヤーのパラメータ設定。")]
        private PlayerParametor _parametor;
        private Rigidbody _rb;
        private Animator _animator;
        private Vector3 _direction;
        private IInteractable _currentInteractableObject;
        /// <summary>
        ///     必要なコンポーネントを取得する。
        /// </summary>
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.Log($"{_animator}コンポーネントがアタッチされていません");
            }
        }

        /// <summary>
        ///     移動方向と速度から物理移動を反映する。
        /// </summary>
        private void FixedUpdate()
        {
            var v = _direction * _parametor.MoveSpeed;
            _rb.linearVelocity = new Vector3(v.x, _rb.linearVelocity.y, v.z);

            var flat = new Vector3(_direction.x, 0f, _direction.z);
            if (flat.sqrMagnitude < 0.001f)
            {
                return;
            }

            var target = Quaternion.LookRotation(flat);
            _rb.MoveRotation(Quaternion.RotateTowards(_rb.rotation, target, 720 * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out var interactable))
            {
                _currentInteractableObject = interactable;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out var interactable))
            {
                _currentInteractableObject = null;
            }
        }
    }
}
