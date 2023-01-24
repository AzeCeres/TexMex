using UnityEngine;

namespace OmniDi.Library.CharacterControl.Input
{
    /// <summary>
    /// A generic abstract class that implements the chain-of-command design pattern for MonoBehaviours.
    /// </summary>
    public abstract class InputProvider : MonoBehaviour
    {
        protected InputProvider Next;

        /// <summary>
        /// Returns a struct containing the current input that should be executed be the character controller.
        /// </summary>
        /// <param name="currentState">The current input that should be edited.</param>
        /// <returns>The input that should be executed by the character controller.</returns>
        public InputState GetInput(InputState currentState)
        {
            currentState = EditState(currentState);
            return Next?.GetInput(currentState) ?? currentState;
        }

        /// <summary>
        /// Sets the next InputProvider in the chain.
        /// </summary>
        /// <param name="nextProvider">The next InputProvider in the chain.</param>
        /// <returns>This object with all of it's sub links</returns>
        public InputProvider SetNext(InputProvider nextProvider)
        {
            var lastProvider = this;

            while (lastProvider != null)
            {
                lastProvider = lastProvider.Next;
            }

            lastProvider.Next = nextProvider;
            return this;
        }

        /// <summary>
        /// An abstract method that edits the inputs based on it's functions.
        /// </summary>
        /// <param name="currentState">The current inputs that should be edited.</param>
        /// <returns>The new inputs that should be executed.</returns>
        protected abstract InputState EditState(InputState currentState);

    }
}