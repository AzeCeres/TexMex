using Audio;
using UnityEngine;
namespace Puzzle.Pots
{
    public class PlantSkullBreaking : MonoBehaviour {
        private Animator animator;
        private AudioSource audio;
        [SerializeField] private AudioVariation potBreaking;
        private bool broken = false;
        [SerializeField] private ParticleSystem potSmoke;
        private void Start()
        {
            animator = GetComponent<Animator>();
            audio = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (broken == false);
            {
                animator.Play("Skull_Grass_Broken");
            
                if (broken == false)
                {
                    potSmoke.Play();
                    potBreaking.PlayAudio(audio);
                }
                broken = true;
            }
        }
        public void Reset() {
            broken = false;
            animator.Play("Skull_Grass");
        }
    }
}
