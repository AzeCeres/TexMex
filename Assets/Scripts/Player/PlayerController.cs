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
        [SerializeField]private PlayerAnimator[] m_Animators;
    
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
            foreach (var animator in m_Animators) {
                if (animator.gameObject != m_Split.clones[m_Split.selectedMain].gameObject)
                    continue;
                if (m_Input.moveMainVector.x == 0 && m_Input.moveMainVector.y == 0) 
                    return;
                animator.UpdateAnimator(m_Input.moveMainVector);
            }
            m_Movement.MoveMain(m_Input.moveMainVector);
            m_Movement.MoveSecond(m_Input.moveSecondVector);
        }
    }
}