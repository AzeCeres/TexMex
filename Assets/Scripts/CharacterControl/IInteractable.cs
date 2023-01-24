using UnityEngine;

namespace OmniDi.Library.EnvironmentalControl
{
    /// <summary>
    /// GameObjects that the player can press a button to interact with.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Is to be called by another script whenever the player presses the interact button.
        /// </summary>
        /// <param name="gameObject">The GameObject of the player.</param>
        public void Interact(GameObject playerGameObject);

        /// <summary>
        /// Is to be called whenever the player moves in and out of interactable range to this object and it is selected for interaction.
        /// </summary>
        /// <param name="canBeSelected">If the GameObject is within interactable range of the player.</param>
        public void Selectable(bool canBeSelected);
    }
}