using FMODUnity;
using Player;
using UnityEngine;

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
        }
    }
}