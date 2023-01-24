using OmniDi.Library.CharacterControl.Input;
using OmniDi.Library.Util;
using UnityEngine;

namespace OmniDi.Library.CharacterControl
{
    /// <summary>
    /// An abstract 2D character controller that can be specialized into creating player or AI controlled characters.
    /// </summary>
    [RequireComponent(typeof(CharacterMovement2D))]
    public abstract class CharacterController2D : Pauseable
    {
        [SerializeField] private InputProvider[] inputProviders;

        protected InputProvider Input;
        protected CharacterMovement2D Movement2D;

        protected virtual void Awake()
        {
            Movement2D = GetComponent<CharacterMovement2D>();
            Input = inputProviders[0];
            for (int i = 1; i < inputProviders.Length; i++)
            {
                Input = Input.SetNext(inputProviders[i]);
            }
        }
    }
}