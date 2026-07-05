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
        public bool CanFishing => _canFishing;

        /// <summary>
        ///     移動方向を設定する。
        /// </summary>
        public void Move(Vector3 dir)
        {
            _direction = dir;
        }

        /// <summary>
        ///     釣りを開始する。
        /// </summary>
        public void Fishing()
        {
        }

        /// <summary>
        ///     釣りを終了して戻る。
        /// </summary>
        public void ReturnFishing()
        {
        }

        /// <summary>
        ///     魚を入手する。
        /// </summary>
        public void GetFish()
        {
        }

        [SerializeField, Tooltip("プレイヤーのパラメータ設定。")]
        private PlayerParametor _parametor;

        private Rigidbody _rb;
        private Animator _animator;
        private InputActionAsset _actionAsset;
        private Vector3 _direction;
        private bool _canFishing;
        /// <summary>
        ///     必要なコンポーネントを取得する。
        /// </summary>
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        ///     移動方向と速度から物理移動を反映する。
        /// </summary>
        private void FixedUpdate()
        {
            var v = _direction * _parametor.MoveSpeed;
            _rb.linearVelocity = new Vector3(v.x, _rb.linearVelocity.y, v.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<FishingArea>(out var fishingArea))
            {
                _canFishing = true;
            }
            else
            {
                if (_canFishing)
                {
                    _canFishing = false;
                }
            }
        }
    }
}
