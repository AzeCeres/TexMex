using Audio;
using UnityEngine;
namespace Puzzle.Pots {
    public class PotBreakingScript : MonoBehaviour {
        private Animator animator;
        private AudioSource audio;
        [SerializeField] private AudioVariation potBreaking;
        private bool broken = false;
        private SpriteRenderer renderer;
        [SerializeField] private Sprite pot_broken;
        [SerializeField] private ParticleSystem potSmoke;
        private Sprite _unBrokenSprite;


        private void Awake() {
            renderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            audio = GetComponent<AudioSource>();
        }
        private void Start() {
            _unBrokenSprite = renderer.sprite;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (broken == false); {
                SpriteBroken();
                if (broken == false) {
                    potBreaking.PlayAudio(audio);
                    potSmoke.Play();
                }
                broken = true;
            }
        }
        private void SpriteBroken() {
            renderer.sprite = pot_broken;
        }
        public void Reset() {
            broken = false;
            renderer.sprite = _unBrokenSprite;
        }
    }
}
