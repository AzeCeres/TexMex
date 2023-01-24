using System;
using OmniDi.Library.Util;
using UnityEngine;

namespace OmniDi.Library.CharacterControl
{
    /// <summary>
    /// A generic 2D movement class that can be inherited to create more specialized movement.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement2D : Pauseable
    {
        [SerializeField] protected float movementSpeedModifier = 1f;
        protected float MovementSpeedMultiplier = 20f;

        protected Rigidbody2D Rigidbody;

        private Vector2 _velocity;

        protected virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }


        /// <summary>
        /// Moves the character in the desired direction.
        /// </summary>
        /// <param name="moveVector">The vector to move the character in.</param>
        public virtual void Move(Vector2 moveVector)
        {
            if (paused) return;
            Rigidbody.AddForce(moveVector.normalized * (movementSpeedModifier * MovementSpeedMultiplier), ForceMode2D.Force);
        }

        protected override void Pause()
        {
            base.Pause();
            _velocity = Rigidbody.velocity;
            Rigidbody.velocity = Vector2.zero;
        }

        protected override void UnPause()
        {
            base.UnPause();
            Rigidbody.velocity = _velocity;
        }
    }
}