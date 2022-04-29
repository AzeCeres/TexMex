using System;
using UnityEngine;

namespace Puzzle
{
    public class PoisonDartShooter : MonoBehaviour
    {
        [SerializeField] private AnimationClip poisonDartShoot;
        [SerializeField] private Direction direction = Direction.Down;

        [SerializeField] private float shootDelay = 1f;

        [SerializeField] private GameObject dart;

        [SerializeField] private GameObject animationObject;

        private Animation _animation;
        private Vector2 _shootDirection;
        private Quaternion _lookDirection;

        private float _shootDelayTimer;
        private bool _hasShot;

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private void Start()
        {
            _animation = animationObject.GetComponent<Animation>();

            switch (direction)
            {
                case Direction.Up:
                    _shootDirection = Vector2.up;
                    animationObject.transform.position = Vector2.up;
                    _lookDirection = Quaternion.Euler(0, 0, 90);
                    break;
                case Direction.Down:
                    _shootDirection = Vector2.down;
                    animationObject.transform.position = Vector2.down;
                    _lookDirection = Quaternion.Euler(0, 0, -90);
                    break;
                case Direction.Left:
                    _shootDirection = Vector2.left;
                    animationObject.transform.position = Vector2.left;
                    _lookDirection = Quaternion.Euler(0, 0, 0);
                    break;
                case Direction.Right:
                    _shootDirection = Vector2.right;
                    animationObject.transform.position = Vector2.right;
                    _lookDirection = Quaternion.Euler(0, 0, 180);
                    break;
            }

            animationObject.transform.rotation = _lookDirection;
        }

        private void Update()
        {
            DetectPlayer();
        }

        private void DetectPlayer()
        {
            if (Time.time < _shootDelayTimer) return;
            var hitObject = Physics2D.Raycast(transform.position, _shootDirection);


            if (hitObject.transform.CompareTag("Player"))
            {
                _shootDelayTimer = Time.time + shootDelay;
                _hasShot = false;
            }

            if (_hasShot) return;
            ShootDart();
            _hasShot = true;
        }

        private void ShootDart()
        {
            _animation.clip = poisonDartShoot;
            _animation.Play();

            Instantiate(dart, transform.position, _lookDirection);
        }
    }
}