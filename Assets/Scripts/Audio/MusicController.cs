using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneManager = UnityEngine.SceneManagement.SceneManager;
using STOP_MODE = FMOD.Studio.STOP_MODE;

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

        private int _sceneNumber;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Start()
        {
            _studioEventEmitter = GetComponent<StudioEventEmitter>();
            DontDestroyOnLoad(transform.parent);
            _masterVolume = RuntimeManager.GetBus("bus:/Master");
            _musicVolume = RuntimeManager.GetBus("bus:/Master/Music");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _split = FindObjectOfType<Split>();
            _sceneNumber = SceneManager.GetActiveScene().buildIndex;
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
            switch (_sceneNumber)
            {
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
                default:
                    _masterVolume.stopAllEvents(STOP_MODE.IMMEDIATE);
                    break;
            }
        }

        private void UpdateCloneCount()
        {
            if (_sceneNumber < 1 || _sceneNumber > 3) return;
            
            _numberOfClones = _sceneNumber - 1;
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