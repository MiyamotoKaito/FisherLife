using UnityEngine;

namespace Utility
{
    public class OnTriggerPriorityChange : CinemachineCameraPrioriyChanger
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _cinemachineCamera.Priority = _priority;
                _cinemachineCamera.gameObject.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _cinemachineCamera.Priority = CINEMACHINE_CAMERA_PRIORITY;
                _cinemachineCamera.gameObject.SetActive(false);
            }
        }
    }
}
