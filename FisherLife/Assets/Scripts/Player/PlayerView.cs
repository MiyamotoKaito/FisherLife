using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerModule
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerParametor _parametor;
        private Rigidbody _rb;
        private Animator _animator;
        private InputActionAsset _actionAsset;
        private Vector3 _direction;
        public void Move(Vector3 dir)
        {
            _direction = dir;
        }
        public void Fishing()
        {

        }
        public void ReturnFishing()
        {

        }
        public void GetFish()
        {

        }
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }
        private void FixedUpdate()
        {
            var v = _direction * _parametor.MoveSpeed;

            _rb.linearVelocity = new Vector3(v.x, _rb.linearVelocity.y, v.z);
        }
    }
}