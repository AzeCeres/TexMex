using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class DoorAudioController : MonoBehaviour
    {
        [SerializeField] private AudioVariation doorAudio;
        private static AudioVariation _doorAudio;

        private static AudioSource _audioSource;

        private static float _playOpenDelay;
        private static float _playCloseDelay;

        private void Awake()
        {
            _doorAudio = doorAudio;
            _audioSource = GetComponent<AudioSource>();
        }

        public static void PlayDoorAudio(bool open)
        {
            if (open)
            {
                if (Time.time < _playOpenDelay) return;

                _playOpenDelay = Time.time + _doorAudio.GetClipLength();
            }
            else
            {
                if (Time.time < _playCloseDelay) return;

                _playCloseDelay = Time.time + _doorAudio.GetClipLength();
            }
            

            _doorAudio.PlayAudio(_audioSource);
        }
    }
}