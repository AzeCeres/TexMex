using UnityEngine;

namespace Audio
{
    public abstract class AudioVariation: ScriptableObject
    {
        protected AudioClip _nextClip;

        public abstract void PlayAudio(AudioSource audioSource);

        public virtual float GetClipLength()
        {
            return _nextClip.length;
        }
    }
}