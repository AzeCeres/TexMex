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
            switch (SceneManager.GetActiveScene().name)
            {
                case "MainMenu":
                    _studioEventEmitter.Stop();
                    break;
                case "Level 1":
                    // _studioEventEmitter.Play();
                    _studioEventEmitter.SetParameter("Region", 0);
                    break;
                case "Level 2":
                    // _studioEventEmitter.Play();
                    _studioEventEmitter.SetParameter("Region", 1);
                    break;
                case "Level 3":
                    // _studioEventEmitter.Play();
                    _studioEventEmitter.SetParameter("Region", 2);
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