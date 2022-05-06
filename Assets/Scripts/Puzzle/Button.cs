using Audio;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Puzzle
//todo // Collect colour from Accessibility settings, store colour, and have it be inheritable
{ [RequireComponent(typeof(AudioSource))]
    public class Button : MonoBehaviour {
        [SerializeField] private bool staysDown;
        [SerializeField] private AnimationClip press;
        [SerializeField] private AnimationClip dePress;
        [SerializeField] private AudioVariation pressAudio;
        [SerializeField] private AudioVariation dePressAudio;
        //public Color color;
        public bool active;
        private int m_InsideCount;
        private bool m_WasActive = false;
        private Animator m_Animator;
        private AudioSource m_AudioSource;
        
        private void Awake() {
            m_Animator = GetComponent<Animator>();
            m_AudioSource = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            light2D = GetComponentInChildren<Light2D>();
        }
        [CanBeNull] private Light2D light2D;
        private SpriteRenderer spriteRenderer;

        private void Start() {
            UpdateMaterial();
            LightOn();
        }
        void UpdateMaterial() {
            spriteRenderer.material.SetFloat("_Active",1f);
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
        private void Activate() {
            //todo Sound and Particles
            pressAudio.PlayAudio(m_AudioSource);
            m_Animator.Play(press.name);
        }
        private void DeActivate() {
            //todo Sound and Particles
            dePressAudio.PlayAudio(m_AudioSource);
            m_Animator.Play(dePress.name);
        }
        public void Reset() {
            m_InsideCount = 0;
            m_Animator.Play(dePress.name);
        }
        public void LightOff() {
            light2D.enabled = false;
        }
        public void LightOn() {
            light2D.enabled = true;
        }
    }
}