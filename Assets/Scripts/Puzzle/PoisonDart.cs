using Player;
using UnityEngine;

namespace Puzzle
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PoisonDart : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private AnimationClip hitAnimation;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private Split _split;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);

            var obj = GameObject.FindGameObjectWithTag("PlayerController");
            _split = obj.GetComponent<Split>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag("Player"))
            {
                _split.KillClone(col.gameObject);
            }
            _animator.Play("dart_contact_animation");
        }

        private void DestroySelf()
        {
            Destroy(this);
        }

        private void PlayDartFlyingAnimation()
        {

        }
    }
}