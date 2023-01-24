using System;
using OmniDi.Library.CharacterControl;
using OmniDi.Library.CharacterControl.Input;

namespace Player
{
    public class CloneController : CharacterController2D
    {
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Movement2D.Move(Input.GetInput(new InputState()).GetVector2ValueFromActionName("MoveVector").ValueOrThrow().Value);
        }
    }
}