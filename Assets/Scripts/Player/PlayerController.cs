using Player;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Split))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Input))]
public class PlayerController : MonoBehaviour
{
    private Input _mInput;
    private Movement _mMovement;
    private Split _mSplit;
    private void Awake()
    {
        _mMovement = GetComponent<Movement>();
        _mInput = GetComponent<Input>();
        _mSplit = GetComponent<Split>();
    }
    //calls upon functions to split whenever buttons to split are pressed
    void OnSplitMain(InputValue inputValue) => _mSplit.MainSplit();
    void OnSplitSecond(InputValue inputValue) => _mSplit.SecondSplit();
    private void FixedUpdate()
    {
        _mMovement.MoveMain(_mInput.MoveMainVector);
        _mMovement.MoveSecond(_mInput.MoveSecondVector);
    }
   
}