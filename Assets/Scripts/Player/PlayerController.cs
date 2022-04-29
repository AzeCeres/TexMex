using UnityEngine;
using UnityEngine.InputSystem;
namespace Player {
    [RequireComponent(typeof(Split))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Input))]
    public class PlayerController : MonoBehaviour {
        private Input m_Input;
        private Movement m_Movement;
        private Split m_Split;
        private PlayerInput m_InputManager;
    
        private void Awake() {
            m_Movement = GetComponent<Movement>();
            m_Input = GetComponent<Input>();
            m_Split = GetComponent<Split>();
            m_InputManager = GetComponent<PlayerInput>();
        }
        private void Start() { //todo remove this function, its for testing
            m_InputManager.SwitchCurrentActionMap("AlternativePlayer");
        }
        //calls upon functions to split whenever buttons to split are pressed
        void OnSplitMain(InputValue inputValue) => m_Split.MainSplit();
        void OnSplitSecond(InputValue inputValue) => m_Split.SecondSplit();
        void OnAlternativeSplit(InputValue inputValue) => m_Split.AlternativeSplit();
        void OnAlternativeSwitch(InputValue inputValue) {
            var intInputValue = Mathf.RoundToInt(inputValue.Get<float>());
            m_Split.AlternativeSwitch(intInputValue);
        }
        private void FixedUpdate() {
            m_Movement.MoveMain(m_Input.moveMainVector);
            m_Movement.MoveSecond(m_Input.moveSecondVector);
        }
    }
}