using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class CloneAudio : MonoBehaviour
    {
        [SerializeField] private AudioVariation footStepAudio;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void PlayFootStepAudio()
        {
            footStepAudio.PlayAudio(_audioSource);
        }
    }
}