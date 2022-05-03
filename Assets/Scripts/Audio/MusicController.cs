using FMODUnity;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Audio
{
    [RequireComponent(typeof(StudioEventEmitter))]
    public class MusicController : MonoBehaviour
    {
        private Split _split;

        private StudioEventEmitter _studioEventEmitter;

        private static int _roomNumber;
        private int _numberOfClones;

        private void Start()
        {
            _studioEventEmitter = GetComponent<StudioEventEmitter>();
            _split = FindObjectOfType<Split>();
            DontDestroyOnLoad(transform.parent);
        }

        private void Update()
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
    }
}