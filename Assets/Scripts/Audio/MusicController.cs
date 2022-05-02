using FMODUnity;
using Player;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(StudioEventEmitter))]
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private Split split;

        private StudioEventEmitter _studioEventEmitter;

        private static int _roomNumber;
        private int _numberOfClones;

        private void Start()
        {
            _studioEventEmitter = GetComponent<StudioEventEmitter>();
        }

        private void Update()
        {
            _numberOfClones = 0;
            foreach (var cloneSlot in split.activeClones)
            {
                if (cloneSlot)
                {
                    _numberOfClones += 1;
                }
            }


        }
    }
}