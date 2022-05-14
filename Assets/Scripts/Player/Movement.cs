using UnityEngine;
namespace Player {
    public class Movement : MonoBehaviour {
        private Split _split;
        [Tooltip("The acceleration and max moveSpeed of the player character")]
        [SerializeField] private float moveSpeed = 7f;
        private float _movementMultiplier = 10f; // a multiplier to the moveSpeed in order to keep the numbers relatively low and manageable
        [Tooltip("An array of the clones rigidbodies, it has to be in the same order as the clone list from split. Otherwise it will not work properly")]
        [SerializeField]private Rigidbody2D[] rb;
        [Tooltip("The amount of slowdown on the player. Contributes to how quickly the player stops moving after you stop pressing the direction")]
        [SerializeField]private float drag = 15f;
        private void Awake() {
            _split = GetComponent<Split>();
        }
        private void FixedUpdate() {
            Drag(); // in fixed update to keep it moving at the same speed as the physicsSteps
        }
        private void Drag() {
            for (int i = 0; i < _split.clones.Length; i++) { // Gets the velocity of every single clone, checks if they are active, and applies less drag to dead clones
                Vector2 velocity = transform.InverseTransformDirection(rb[i].velocity);
                float forceX;
                float forceY;
                if (_split.activeClones[i]) {
                    forceX = -drag * velocity.x;
                    forceY = -drag * velocity.y;
                } else {
                    var deathDrag = drag / 2;
                    forceX = -deathDrag * velocity.x;
                    forceY = -deathDrag * velocity.y;
                }
                rb[i].AddRelativeForce(new Vector2(forceX, forceY)); // adds "negative force" to the player, as to work as drag
            }
        }
        // Called from PlayerController, The act of adding force in the direction gotten from the actionMap
        public void MoveMain(Vector2 moveVector) {
            if (_split.mainClones.Count == 0) return;
            //todo make it only happen once
            var mainRb = _split.mainClones[_split.selectedMain].GetComponent<Rigidbody2D>(); // Gets the RB of the specified clone
            mainRb.AddForce(moveVector.normalized * (moveSpeed * _movementMultiplier), ForceMode2D.Force);
        }
    }
}
