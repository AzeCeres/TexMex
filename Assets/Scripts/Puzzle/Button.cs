using Audio;
using UnityEngine;
namespace Puzzle
//todo // Collect colour from Accessibility settings, store colour, and have it be inheritable
{ [RequireComponent(typeof(AudioSource))]
    public class Button : MonoBehaviour {
        [SerializeField] private bool staysDown;
        [SerializeField] private AnimationClip press;
        [SerializeField] private AnimationClip dePress;
        [SerializeField] private AudioVariation pressAudio;
        [SerializeField] private AudioVariation dePressAudio;
        public Color color;
        public bool active;
        private int m_InsideCount;
        private bool m_WasActive = false;
        private Animator m_Animator;
        private AudioSource m_AudioSource;

        private void Awake() {
            m_Animator = GetComponent<Animator>();
            m_AudioSource = GetComponent<AudioSource>();
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if(!other.gameObject.CompareTag("Player")) return;
            m_InsideCount++;
        }
        private void OnTriggerExit2D(Collider2D other) {
            if(!other.gameObject.CompareTag("Player")) return;
            if (staysDown) return;
            m_InsideCount--;
        }
        private void Update() {
            m_WasActive = active;
            active = m_InsideCount>0;
            if (active && !m_WasActive)
                Activate();
            else if (!active && m_WasActive)
                DeActivate();
            m_WasActive = active;
        }
        void Activate() {
            //todo Sound and Particles
            pressAudio.PlayAudio(m_AudioSource);
            m_Animator.Play(press.name);
        }
        void DeActivate() {
            //todo Sound and Particles
            dePressAudio.PlayAudio(m_AudioSource);
            m_Animator.Play(dePress.name);
        }
    }
}