using UnityEngine;
using UnityEngine.InputSystem;
namespace Player {
    [RequireComponent(typeof(Split))]
    [RequireComponent(typeof(Movement))]
    public class PlayerController : MonoBehaviour {
        private Input m_Input;
        private Movement m_Movement;
        private Split m_Split;
        [SerializeField]private PlayerAnimator[] m_Animators;
    
        private void Awake() {
            m_Movement = GetComponent<Movement>();
            m_Input = GetComponent<Input>();
            m_Split = GetComponent<Split>();
        }
        void OnAlternativeSplit(InputValue inputValue) => m_Split.AlternativeSplit();
        void OnAlternativeSwitch(InputValue inputValue) {
            var intInputValue = Mathf.RoundToInt(inputValue.Get<float>());
            m_Split.AlternativeSwitch(intInputValue);
        }
        private void FixedUpdate() {
            foreach (var animator in m_Animators) {
                if (animator.gameObject != m_Split.mainClones[m_Split.selectedMain].gameObject)
                    continue;
                if (m_Input.moveMainVector.x == 0 && m_Input.moveMainVector.y == 0) 
                    return;
            }
            m_Movement.MoveMain(m_Input.moveMainVector);
            m_Movement.MoveSecond(m_Input.moveSecondVector);
        }
    }
}