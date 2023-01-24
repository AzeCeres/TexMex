using OmniDi.Library.EnvironmentalControl;
using UnityEngine;
using OmniDi.Library.Wrappers;

namespace OmniDi.Library.CharacterControl
{
    /// <summary>
    /// MonoBehaviour that handles the actions of when the player presses the interact button.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField]
        private Tag interactableTag = new("Interactable");

        private bool _canInteract;
        private Option<IInteractable> _interactableObject;

        // Gets a reference to the interactable object and tells it that it's in an interactable state.
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!interactableTag.CompareTag(col))
                return;
            if (_interactableObject.HasValue)
            {
                _interactableObject.Value.Selectable(false);
            }

            _canInteract = true;
            _interactableObject.Value = col.GetComponent<IInteractable>();
            _interactableObject.Value.Selectable(true);
        }

        // Removes the reference to the interactable object and tells it that it's no longer in an interactable state.
        private void OnTriggerExit2D(Collider2D col)
        {
            if (!interactableTag.CompareTag(col))
                return;

            _canInteract = false;
            if (!_interactableObject.HasValue) return;
            _interactableObject.Value.Selectable(false);
            _interactableObject.HasValue = false;
        }

        /// <summary>
        /// Performs the interact function on an interactable object if such object is within interactable range of the player.
        /// </summary>
        public void Interact()
        {
            if (!_canInteract)
                return;
            _interactableObject.Value.Interact(gameObject);
        }
    }
}