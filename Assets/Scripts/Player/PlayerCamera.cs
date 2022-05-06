using Cinemachine;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Split))]
    public class PlayerCamera : MonoBehaviour {
        [SerializeField] private CinemachineFreeLook[] cameras;
        private Split _split;
        private void Awake() {
            _split = GetComponent<Split>();
        }
        private void Update()
        {
            UpdateCameraPriorities();
        }
        

        private void UpdateCameraPriorities()
        {
            foreach (var camera in cameras)
                camera.m_Priority = camera.transform.parent.gameObject == _split.mainClones[_split.selectedMain] ? 10 : 0;
        }
    }
}