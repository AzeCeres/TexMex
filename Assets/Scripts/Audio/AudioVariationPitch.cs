using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Audio Variation Pitch", menuName = "TexMex/Audio Variation Pitch", order = 1)]
    public class AudioVariationPitch : ScriptableObject, IAudioVariation
    {
        [SerializeField] private float pitchLowerBound = 1f;
        [SerializeField] private float pitchUpperBound = 1f;
        [SerializeField] private AudioClip audioClip;

        public void PlayAudio(AudioSource audioSource)
        {
            var pitch = Random.Range(pitchLowerBound, pitchUpperBound);
            audioSource.pitch = pitch;

            audioSource.PlayOneShot(audioClip);
            audioSource.pitch = 1f;
        }
    }
}