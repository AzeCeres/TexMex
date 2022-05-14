using Unity.VisualScripting.FullSerializer;
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

            _nextClip = audioClips[clipIndex];

            audioSource.PlayOneShot(_nextClip);
        }
        
        public override float GetClipLength()
        {
            if (_nextClip != false) return _nextClip.length;
            
            var clipIndex = Random.Range(0, audioClips.Length);
            _nextClip = audioClips[clipIndex];
            
            return _nextClip.length;
        }
    }
}