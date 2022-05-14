using UnityEngine;
using UnityEngine.InputSystem;
namespace Player {
    [RequireComponent(typeof(Split))]
    [RequireComponent(typeof(Movement))]
    public class PlayerController : MonoBehaviour {
        private Input _input;
        private Movement _movement;
        private Split _split;
        [SerializeField]private PlayerAnimator[] animators;
    
        private void Awake() {
            _movement = GetComponent<Movement>();
            _input = GetComponent<Input>();
            _split = GetComponent<Split>();
        }
        void OnAlternativeSplit(InputValue inputValue) => _split.AlternativeSplit();
        void OnAlternativeSwitch(InputValue inputValue) {
            var intInputValue = Mathf.RoundToInt(inputValue.Get<float>());
            _split.AlternativeSwitch(intInputValue);
        }
        private void FixedUpdate() {
            foreach (var animator in animators) {
                if (animator.gameObject != _split.mainClones[_split.selectedMain].gameObject)
                    continue;
                if (_input.moveMainVector.x == 0 && _input.moveMainVector.y == 0) 
                    return;
            }
            _movement.MoveMain(_input.moveMainVector);
            //m_Movement.MoveSecond(m_Input.moveSecondVector);
        }
    }
}