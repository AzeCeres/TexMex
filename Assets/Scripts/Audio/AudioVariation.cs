using UnityEngine;

namespace Audio
{
    public abstract class AudioVariation: ScriptableObject
    {
        public abstract void PlayAudio(AudioSource audioSource);
    }
}