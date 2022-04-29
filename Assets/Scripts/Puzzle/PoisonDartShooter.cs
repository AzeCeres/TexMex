using System;
using UnityEngine;

namespace Puzzle
{
    public class PoisonDartShooter : MonoBehaviour
    {
        [SerializeField] private Direction direction = Direction.Down;

        [SerializeField] private float shootDelay = 1f;

        [SerializeField] private GameObject dart;

        [SerializeField] private GameObject animationObject;

        private Animator _animator;
        private Vector2 _shootDirection;
        private Vector2 _dartSpawnPosition;
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
            _animator = animationObject.GetComponent<Animator>();

            switch (direction)
            {
                case Direction.Up:
                    _shootDirection = Vector2.up;
                    animationObject.transform.position = Vector2.up;
                    _lookDirection = Quaternion.Euler(0, 0, 90);
                    _dartSpawnPosition = transform.position + new Vector3(0f, 1f, 0f);
                    break;
                case Direction.Down:
                    _shootDirection = Vector2.down;
                    animationObject.transform.position = Vector2.down;
                    _lookDirection = Quaternion.Euler(0, 0, -90);
                    _dartSpawnPosition = transform.position + new Vector3(0f, -1f, 0f);
                    break;
                case Direction.Left:
                    _shootDirection = Vector2.left;
                    animationObject.transform.position = Vector2.left;
                    _lookDirection = Quaternion.Euler(0, 0, 0);
                    _dartSpawnPosition = transform.position + new Vector3(1f, 0f, 0f);
                    break;
                case Direction.Right:
                    _shootDirection = Vector2.right;
                    animationObject.transform.position = Vector2.right;
                    _lookDirection = Quaternion.Euler(0, 0, 180);
                    _dartSpawnPosition = transform.position + new Vector3(-1f, 0f, 0f);
                    break;
            }

            animationObject.transform.position = _dartSpawnPosition;
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

            if (hitObject.collider != null)
            {
                if (hitObject.transform.CompareTag("Player"))
                {
                    _shootDelayTimer = Time.time + shootDelay;
                    _hasShot = false;
                }
            }

            if (_hasShot) return;
            ShootDart();
            _hasShot = true;
        }

        private void ShootDart()
        {
            _animator.Play("dart_origin_animation");

            Instantiate(dart, transform.position, _lookDirection);
        }
    }
}