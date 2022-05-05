using UnityEngine;
using UnityEngine.Audio;

namespace System
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private SettingsController settings;
        [SerializeField] private AudioMixer audioMixer;

        private void Update()
        {
            audioMixer.SetFloat("Master",settings.masterVolume);
            audioMixer.SetFloat("Environment SFX", settings.environmentVolume);
            audioMixer.SetFloat("Player SFX", settings.playerVolume);
        }
    }
}