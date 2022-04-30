using Audio;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioVariation cloneCreateAudio;
    [SerializeField] private AudioVariation cloneSwitchAudio;
    [Space]
    [SerializeField] private AudioVariation cloneDeathAudio;
    [SerializeField] private AudioVariation cloneDeathBarkLaser;
    [SerializeField] private AudioVariation cloneDeathBarkDart;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayCloneCreateAudio()
    {
        cloneCreateAudio.PlayAudio(_audioSource);
    }

    public void PlayCloneSwitchAudio()
    {
        cloneSwitchAudio.PlayAudio(_audioSource);
    }

    public void PlayCloneDeathAudio()
    {
        cloneDeathAudio.PlayAudio(_audioSource);
        cloneDeathBarkDart.PlayAudio(_audioSource);
    }
}