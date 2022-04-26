using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioTest : MonoBehaviour
    {
        [SerializeField] private AudioVariation audioVariationArray;
        [SerializeField] private AudioVariation audioVariationPitch;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                audioVariationArray.PlayAudio(_audioSource);
            }

            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                audioVariationPitch.PlayAudio(_audioSource);
            }
        }
    }
}