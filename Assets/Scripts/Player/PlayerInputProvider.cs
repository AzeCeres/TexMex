using OmniDi.Library.CharacterControl.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputProvider : InputProvider
    {
        private CharacterInputValue<Vector2> _moveVector = new(Vector2.zero, "MoveVector");
        private CharacterInputValue<bool> _clone = new(false, "Clone");
        private CharacterInputValue<bool> _switch = new(false, "Switch");
        private CharacterInputValue<bool> _pause = new(false, "Pause");
        private CharacterInputValue<bool> _interact = new(false, "Interact");

        protected override InputState EditState(InputState currentState)
        {
            var state = new InputState();
            state.AddValue(_moveVector);
            state.AddValue(_clone);
            state.AddValue(_switch);
            state.AddValue(_pause);
            state.AddValue(_interact);

            _clone.Value = false;
            _switch.Value = false;
            _pause.Value = false;
            _interact.Value = false;

            return state;
        }

        private void OnMove(InputValue inputValue) => _moveVector.Value = inputValue.Get<Vector2>();
        private void OnClone() => _clone.Value = true;
        private void OnSwitch() => _switch.Value = true;
        private void OnPause() => _pause.Value = true;
        private void OnInteract() => _interact.Value = true;


    }
}