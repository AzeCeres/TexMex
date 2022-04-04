using Unity.VisualScripting;
using UnityEngine;
namespace Puzzle
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private bool staysDown;
        [SerializeField] private bool keptDown;

        public bool active;
        private void OnTriggerStay2D(Collider2D other) {
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
