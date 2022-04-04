using Player;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Split))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Input))]
public class PlayerController : MonoBehaviour
{
    private Input m_Input;
    private Movement m_Movement;
    private Split m_Split;
    private void Awake()
    {
        m_Movement = GetComponent<Movement>();
        m_Input = GetComponent<Input>();
        m_Split = GetComponent<Split>();
    }
    //calls upon functions to split whenever buttons to split are pressed
    void OnSplitMain(InputValue inputValue) => m_Split.MainSplit();
    void OnSplitSecond(InputValue inputValue) => m_Split.SecondSplit();
    private void FixedUpdate()
    {
        m_Movement.MoveMain(m_Input.moveMainVector);
        m_Movement.MoveSecond(m_Input.moveSecondVector);
    }
   
}