using System.Collections;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class LaserAudioController : MonoBehaviour
    {
        [SerializeField] private AudioClip laserFire;
        [SerializeField] private AudioClip laserIdle;

        [SerializeField] private bool startActive = true;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            if (startActive) PlayLaserIdleAudio();
        }

        public void PlayLaserFireAudio()
        {
            _audioSource.loop = false;
            _audioSource.clip = laserFire;
            _audioSource.Play();

            StartCoroutine(WaitForClip());
        }

        public void StopAudio()
        {
            _audioSource.Stop();
        }

        private void PlayLaserIdleAudio()
        {
            _audioSource.clip = laserIdle;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        IEnumerator WaitForClip()
        {
            var stopAtTime = Time.time + laserFire.length;

            while (Time.time < stopAtTime)
            {
                yield return null;
            }
            PlayLaserIdleAudio();
        }
    }
}