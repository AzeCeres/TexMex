using Player;
using UnityEngine;
namespace Puzzle
{
    public class Laser : MonoBehaviour {
        private Split _mSplit;
        private void Start() {
            var obj = GameObject.FindGameObjectWithTag("PlayerController");
            _mSplit = obj.GetComponent<Split>();
        }
        private void OnTriggerEnter2D(Collider2D other) {
            var obj = other.gameObject;
            _mSplit.KillClone(obj);
        }
    }
}
