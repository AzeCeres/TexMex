using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Audio
{
    [RequireComponent(typeof(LaserAudioController))]
    public class AudioTest : MonoBehaviour
    {
        private LaserAudioController _laserAudioController;

        private void Start()
        {
            _laserAudioController = GetComponent<LaserAudioController>();
        }

        private void Update()
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                _laserAudioController.PlayLaserFireAudio();
            }

            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                _laserAudioController.StopAudio();
            }
        }
    }
}