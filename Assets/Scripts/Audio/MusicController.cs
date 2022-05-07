using System.Collections;
using FMOD.Studio;
using FMODUnity;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Audio
{
    [RequireComponent(typeof(StudioEventEmitter))]
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private SettingsController settings;
        
        private Split _split;

        private StudioEventEmitter _studioEventEmitter;

        private static int _roomNumber;
        private int _numberOfClones;

        private Bus _masterVolume;
        private Bus _musicVolume;

        private bool _fading;

        private void Start()
        {
            _studioEventEmitter = GetComponent<StudioEventEmitter>();
            _split = FindObjectOfType<Split>();
            DontDestroyOnLoad(transform.parent);
            _masterVolume = RuntimeManager.GetBus("bus:/Master");
            _musicVolume = RuntimeManager.GetBus("bus:/Master/Music");
        }

        private void Update()
        {
            UpdateVolume();
            UpdateCloneCount();
            UpdateRegionNumber();
        }

        public void FadeMusic()
        {
            _fading = true;
        }

        private float Map(float input, float lowerInput, float upperInput, float lowerOutput, float upperOutput)
        {
            return ((input - lowerInput) / (upperInput - lowerInput))
                * (upperOutput - lowerOutput) + lowerOutput;
        }

        private void UpdateVolume()
        {
            var masterVolume = Map(settings.masterVolume, -80f, 0f, 0f, 1f);
            
            _masterVolume.setVolume(masterVolume);
            _musicVolume.setVolume(settings.musicVolume);
        }

        private void UpdateRegionNumber()
        {
            if (_fading) return;
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                    _studioEventEmitter.Stop();
                    break;
                case 1:
                    _studioEventEmitter.SetParameter("Region", 0);
                    break;
                case 2:
                    _studioEventEmitter.SetParameter("Region", 1);
                    break;
                case 3 when !_fading:
                    _studioEventEmitter.SetParameter("Region", 2);
                    break;
                case 3 when _fading:
                    _studioEventEmitter.SetParameter("Region", 3);
                    break;
            }
        }

        private void UpdateCloneCount()
        {
            _numberOfClones = 0;
            foreach (var cloneSlot in _split.activeClones)
            {
                if (cloneSlot)
                {
                    _numberOfClones += 1;
                }
            }
            _studioEventEmitter.SetParameter("Clone", _numberOfClones - 1);
        }
    }
}