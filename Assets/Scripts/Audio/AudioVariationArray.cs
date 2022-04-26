using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Audio Variation Array", menuName = "TexMex/Audio Variation Array", order = 0)]
    public class AudioVariationArray : AudioVariation
    {
        [SerializeField] private AudioClip[] audioClips;

        public override void PlayAudio(AudioSource audioSource)
        {
            var clipIndex = Random.Range(0, audioClips.Length);

            audioSource.PlayOneShot(audioClips[clipIndex]);
        }
    }
}