using UnityEngine;
namespace Player {
    public class PlayerAnimator : MonoBehaviour {
        private Animator m_Animator;
        private Rigidbody2D m_RigidBody;
        private float minSpeedToChange = 0.8f;
        private Split split;
        private bool dead;
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
            split = GetComponent<Split>();
            m_RigidBody = GetComponent<Rigidbody2D>();
            m_Animator = GetComponentInParent<Animator>();
        }
        private void Update() {
            StateCheck();
            if (dead) StopControl();
        }
        private void StateCheck() {
            switch (states) {
                case State.Idle:
                    Idle();
                    break;
                case State.Walk:
                    Walk();
                    break;
            }
            void Idle() {
                if (!(m_RigidBody.velocity.magnitude > 0.3f)) return;
                states = State.Walk;
            }
            void Walk() {
                if (!(m_RigidBody.velocity.magnitude < 0.3f)) return;
                states = State.Idle;
            }
            DirectionCheck(states);
        }
        private void DirectionCheck(State state) {
            var localVel = transform.InverseTransformDirection(m_RigidBody.velocity);
            switch (directions) {
                case Direction.Up:
                    Up();
                    break;
                case Direction.Down:
                    Down();
                    break;
                case Direction.Left:
                    Left();
                    break;
                case Direction.Right:
                    Right();
                    break;
            }
            void Up() {
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
            void Down() {
                if (-localVel.y >= minSpeedToChange && -localVel.y >= Mathf.Abs(localVel.x)) {
                    directions = Direction.Down;
                } else if (localVel.y >= minSpeedToChange && localVel.y >= Mathf.Abs(localVel.x)) {
                    directions = Direction.Up;  
                } else if (-localVel.x >= minSpeedToChange && -localVel.x >= Mathf.Abs(localVel.y)) {
                    directions = Direction.Left;
                } else if (localVel.x >= minSpeedToChange && localVel.x >= Mathf.Abs(localVel.y)) {
                    directions = Direction.Right;
                }
                PlayAnimation(state, directions);
            }
            void Left() {
                if (-localVel.x >= minSpeedToChange && -localVel.x >= Mathf.Abs(localVel.y)) {
                    directions = Direction.Left;
                } if (localVel.y >= minSpeedToChange && localVel.y >= Mathf.Abs(localVel.x)) {
                    directions = Direction.Up;
                } else if (-localVel.y >= minSpeedToChange && -localVel.y >= Mathf.Abs(localVel.x)) {
                    directions = Direction.Down;
                } else if (localVel.x >= minSpeedToChange && localVel.x >= Mathf.Abs(localVel.y)) {
                    directions = Direction.Right;
                }   
                PlayAnimation(state, directions);
            }
            void Right() {
                if (localVel.x >= minSpeedToChange && localVel.x >= Mathf.Abs(localVel.y)) {
                    directions = Direction.Right;
                }else if (localVel.y >= minSpeedToChange && localVel.y >= Mathf.Abs(localVel.x)) {
                    directions = Direction.Up;
                }else if (-localVel.y >= minSpeedToChange && -localVel.y >= Mathf.Abs(localVel.x)) {
                    directions = Direction.Down;
                }else if (-localVel.x >= minSpeedToChange && -localVel.x >= Mathf.Abs(localVel.y)) {
                    directions = Direction.Left;
                }
                PlayAnimation(state, directions);
            }
        }
        private void PlayAnimation(State state, Direction direction) {
            if (dead) return;
            states = state;
            directions = direction;
            switch (state) {
                case State.Idle:
                    switch (direction) {
                        case Direction.Up:
                            m_Animator.Play("player_idle_up");
                            break;
                        case Direction.Down:
                            m_Animator.Play("player_idle_down");
                            break;
                        case Direction.Left:
                            m_Animator.Play("player_idle_left");
                            break;
                        case Direction.Right:
                            m_Animator.Play("player_idle_right");
                            break;
                    }
                    break;
                case State.Walk:
                    switch (direction) {
                        case Direction.Up:
                            m_Animator.Play("player_walk_up");
                            break;
                        case Direction.Down:
                            m_Animator.Play("player_walk_down");
                            break;
                        case Direction.Left:
                            m_Animator.Play("player_walk_left");
                            break;
                        case Direction.Right:
                            m_Animator.Play("player_walk_right");
                            break;
                    }
                    break;
            }
        }
        public void Death() {
            m_Animator.Play("player_death");
            dead = true;
        }
        void StopControl() {
            m_RigidBody.velocity = Vector2.up * 0;
        }
        public void Die() {
            split.KillClone(gameObject);
            dead = false;
        }
    }
}
