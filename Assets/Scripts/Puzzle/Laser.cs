using Player;
using UnityEngine;
namespace Puzzle
{
    public class Laser : MonoBehaviour {
        private Split m_Split;
        private void Start() {
            var obj = GameObject.FindGameObjectWithTag("PlayerController");
            m_Split = obj.GetComponent<Split>();
        }
        private void OnTriggerEnter2D(Collider2D other) {   
            var obj = other.gameObject;
            m_Split.KillClone(obj);
        }
    }
}
