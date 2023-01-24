using System;
using OmniDi.Library.CharacterControl;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : CharacterMovement2D
    {
        [Tooltip("The ID of this clone")]
        [SerializeField] private uint cloneId;
        [SerializeField]private float drag = 15f;

        private PlayerControls _playerControls;

        protected override void Start()
        {
            base.Start();

            _playerControls = PlayerControls.Instance;
        }

        private void FixedUpdate()
        {
            Drag();
        }

        public override void Move(Vector2 moveVector)
        {
            if (_playerControls.CurrentlySelectedCloneId != cloneId) return;
            if (paused) return;

            Rigidbody.AddForce(moveVector.normalized * (movementSpeedModifier * MovementSpeedMultiplier), ForceMode2D.Force);
        }

        private void Drag() {
            Vector2 velocity = transform.InverseTransformDirection(Rigidbody.velocity);
            var force = Vector2.zero;

            if (_playerControls.CurrentlySelectedCloneId == cloneId)
            {
                force.x = -drag * velocity.x;
                force.x = -drag * velocity.y;

                Rigidbody.AddRelativeForce(force); // adds "negative force" to the player, as to work as drag

                return;
            }
            var deathDrag = drag / 2;
            force.x = -deathDrag * velocity.x;
            force.x = -deathDrag * velocity.y;

            Rigidbody.AddRelativeForce(force); // adds "negative force" to the player, as to work as drag
        }
    }
}