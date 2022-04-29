using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Audio
{
    [RequireComponent(typeof(PlayerAudio))]
    public class AudioTest : MonoBehaviour
    {
        private PlayerAudio _playerAudio;

        private void Start()
        {
            _playerAudio = GetComponent<PlayerAudio>();
        }

        private void Update()
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                _playerAudio.PlayCloneCreateAudio();
            }

            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                _playerAudio.PlayCloneSwitchAudio();
            }

            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                _playerAudio.PlayCloneDeathAudio(CauseOfDeath.Dart);
            }

            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                _playerAudio.PlayCloneDeathAudio(CauseOfDeath.Laser);
            }
        }
    }
}