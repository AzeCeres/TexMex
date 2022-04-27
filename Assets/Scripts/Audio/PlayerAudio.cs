using Audio;
using UnityEngine;

public enum CauseOfDeath
{
    Laser,
    Dart
}

[RequireComponent(typeof(AudioSource))]

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioVariation footStepAudio;
    [SerializeField] private AudioVariation cloneCreateAudio;
    [SerializeField] private AudioVariation cloneSwitchAudio;
    [Space]
    [SerializeField] private AudioVariation cloneDeathAudio;
    [SerializeField] private AudioVariation cloneDeathBarkLaser;
    [SerializeField] private AudioVariation cloneDeathBarkDart;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
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

    public void PlayCloneDeathAudio(CauseOfDeath causeOfDeath)
    {
        cloneDeathAudio.PlayAudio(_audioSource);

        switch (causeOfDeath)
        {
            case CauseOfDeath.Laser:
                cloneDeathBarkLaser.PlayAudio(_audioSource);
                break;
            case CauseOfDeath.Dart:
                cloneDeathBarkDart.PlayAudio(_audioSource);
                break;
        }
    }

    private void PlayFootStepAudio()
    {
        footStepAudio.PlayAudio(_audioSource);
    }
}