using Unity.Cinemachine;
using UnityEngine;

namespace Utility
{
    public class CinemachineCameraPrioriyChanger : MonoBehaviour
    {
        protected const int CINEMACHINE_CAMERA_PRIORITY = 0;

        [SerializeField, Tooltip("使用するシネマティックカメラ。")]
        protected CinemachineCamera _cinemachineCamera;
        [SerializeField, Tooltip("カメラの優先度。"), Min(2)]
        protected int _priority = 2;
    }
}