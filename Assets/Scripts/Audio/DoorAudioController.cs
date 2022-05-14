using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class DoorAudioController : MonoBehaviour
    {
        [SerializeField] private AudioVariation doorAudio;
        private static AudioVariation _doorAudio;

        private static AudioSource _audioSource;

        private static float _playDelay;

        private void Awake()
        {
            _doorAudio = doorAudio;
            _audioSource = GetComponent<AudioSource>();
        }

        public static void PlayDoorAudio()
        {
            if (Time.time < _playDelay) return;

            _playDelay = Time.time + _doorAudio.GetClipLength();

            _doorAudio.PlayAudio(_audioSource);
        }
    }
}