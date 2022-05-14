using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Audio Variation Pitch", menuName = "TexMex/Audio Variation Pitch", order = 1)]
    public class AudioVariationPitch : AudioVariation
    {
        [SerializeField] [Range(0f, 1f)] private float pitchLowerBound = 1f;
        [SerializeField] [Range(1f, 2f)] private float pitchUpperBound = 1f;
        [SerializeField] private AudioClip audioClip;

        public override void PlayAudio(AudioSource audioSource)
        {
            var pitch = Random.Range(pitchLowerBound, pitchUpperBound);
            audioSource.pitch = pitch;

            _nextClip = audioClip;

            audioSource.PlayOneShot(audioClip);
            audioSource.pitch = 1f;
        }

        public override float GetClipLength()
        {
            if (_nextClip == null)
            {
                _nextClip = audioClip;
            }

            return _nextClip.length;
        }
    }
}