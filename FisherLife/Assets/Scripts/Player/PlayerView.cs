using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace PlayerModule
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        private Rigidbody _rb;
        private Animator _animator;
        private InputActionAsset _actionAsset;
        public void Move(Vector3 dir)
        {
            _rb.linearVelocity = dir;
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
    }
}