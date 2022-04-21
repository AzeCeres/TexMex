using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Audio Variation Array", menuName = "TexMex/Audio Variation Array", order = 0)]
    public class AudioVariationArray : ScriptableObject, IAudioVariation
    {
        [SerializeField] private AudioClip[] audioClips;

        public void PlayAudio(AudioSource audioSource)
        {
            var clipIndex = Random.Range(0, audioClips.Length);

            audioSource.PlayOneShot(audioClips[clipIndex]);
        }
    }
}