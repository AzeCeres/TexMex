using System;
using Player;
using UnityEngine;

namespace Puzzle
{
    [RequireComponent(typeof(Animation))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PoisonDart : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private AnimationClip hitAnimation;

        private Animation _animation;
        private Rigidbody2D _rigidbody;
        private Split _split;

        private void Start()
        {
            _animation = GetComponent<Animation>();
            _animation.Play();
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.AddForce(new Vector2(moveSpeed, 0f), ForceMode2D.Impulse);

            var obj = GameObject.FindGameObjectWithTag("PlayerController");
            _split = obj.GetComponent<Split>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag("Player"))
            {
                _split.KillClone(col.gameObject);
            }
        }

        private void DestroySelf()
        {
            Destroy(this);
        }
    }
}