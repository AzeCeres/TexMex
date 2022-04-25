using Unity.VisualScripting;
using UnityEngine;
namespace Puzzle
//todo // Collect colour from Accessibility settings, store colour, and have it be inheritable
{ public class Button : MonoBehaviour {
        [SerializeField] private bool staysDown;
        [SerializeField] private bool keptDown;
        public Color color;
        public bool active;
        private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.CompareTag("Player"))
                active = true;
        }
        
        private void OnTriggerExit2D(Collider2D other) {
            if (staysDown) return;
            else {
                active = false;
            }
        }
    }
}
