using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    public Vector2 MoveMainVector { get; private set; }
    void OnMoveMain(InputValue inputValue) => MoveMainVector = inputValue.Get<Vector2>();
    public Vector2 MoveSecondVector { get; private set; }
    void OnMoveSecond(InputValue inputValue) => MoveSecondVector = inputValue.Get<Vector2>();
}