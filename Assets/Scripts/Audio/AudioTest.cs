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

        }
    }
}