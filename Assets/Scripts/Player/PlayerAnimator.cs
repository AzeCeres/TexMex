using UnityEngine;
namespace Player {
    public class PlayerAnimator : MonoBehaviour {
        private Animator m_Animator;
        private Rigidbody2D m_RigidBody;
        private float minSpeedToChange = 0.8f;
        private Split split;
        [SerializeField] private bool dead;
        private enum Direction {
            Up,
            Down,
            Left,
            Right
        }
        private Direction directions;
        private enum State {
            Walk,
            Idle
        }
        private State states;
        private void Awake() {
            split = GetComponentInParent<Split>();
            m_RigidBody = GetComponent<Rigidbody2D>();
            m_Animator = GetComponentInParent<Animator>();
        }
        private void Update() {
            if (dead)
            {
                
                for (int i = 0; i < split.mainClones.Count; i++) {
                    if (gameObject == split.mainClones[i] && i == split.selectedMain) {
                        dead = false;
                    }
                }
                if (dead) return;
            }
            
            StateCheck();
        }
        
        private void StateCheck()
        {
            states = m_RigidBody.velocity.magnitude < 0.4f ? State.Idle : State.Walk;
            DirectionCheck(states);
        }
        private void DirectionCheck(State state) {
            var localVel = transform.InverseTransformDirection(m_RigidBody.velocity);
            
            if (localVel.y >= minSpeedToChange && localVel.y >= Mathf.Abs(localVel.x)) {
                    directions = Direction.Up;
            } else if (localVel.y <= -minSpeedToChange && -localVel.y >= Mathf.Abs(localVel.x)) {
                directions = Direction.Down;
            } else if (-localVel.x >= minSpeedToChange && -localVel.x >= Mathf.Abs(localVel.y)) {
                directions = Direction.Left;
            } else if (localVel.x >= minSpeedToChange && localVel.x >= Mathf.Abs(localVel.y)) {
                directions = Direction.Right;
            }
            PlayAnimation(state, directions);
        }
        private void PlayAnimation(State state, Direction direction) {
            if (dead) return;
            states = state;
            directions = direction;
            switch (state) {
                case State.Idle:
                    switch (direction) {
                        case Direction.Up:
                            m_Animator.Play("idle_up");
                            break;
                        case Direction.Down:
                            m_Animator.Play("idle_down");
                            break;
                        case Direction.Left:
                            m_Animator.Play("idle_left");
                            break;
                        case Direction.Right:
                            m_Animator.Play("idle_right");
                            break;
                    }
                    break;
                case State.Walk:
                    switch (direction) {
                        case Direction.Up:
                            m_Animator.Play("walk_up");
                            break;
                        case Direction.Down:
                            m_Animator.Play("walk_down");
                            break;
                        case Direction.Left:
                            m_Animator.Play("walk_left");
                            break;
                        case Direction.Right:
                            m_Animator.Play("walk_right");
                            break;
                    }
                    break;
            }
        }
        public void Death() {
            if (dead) return;
            m_Animator.Play("death");
            dead = true;
            split.KillClone(gameObject);
        }
        
        public void Die() {
            dead = false;
            split.DeActivateClone(gameObject);
        }
    }
}