using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour {    
    public Vector2 moveMainVector { get; private set; }
    void OnMoveMain(InputValue inputValue) => moveMainVector = inputValue.Get<Vector2>();
    public Vector2 moveSecondVector { get; private set; }
    void OnMoveSecond(InputValue inputValue) => moveSecondVector = inputValue.Get<Vector2>();
}