using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Audio Variation Array Pitch", menuName = "TexMex/Audio Variation Array Pitch", order = 2)]
    public class AudioVariationArrayPitch : AudioVariation
    {
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField] [Range(0f, 1f)] private float pitchLowerBound = 1f;
        [SerializeField] [Range(1f, 2f)] private float pitchUpperBound = 1f;

        public override void PlayAudio(AudioSource audioSource)
        {
            var clipIndex = Random.Range(0, audioClips.Length);
            var pitch = Random.Range(pitchLowerBound, pitchUpperBound);
            audioSource.pitch = pitch;

            audioSource.PlayOneShot(audioClips[clipIndex]);
            audioSource.pitch = 1f;
        }
    }
}