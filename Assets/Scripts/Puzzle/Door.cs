using System.ComponentModel;
using Audio;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Puzzle {
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour {
        [SerializeField] private AnimationClip open;
        [SerializeField] private AnimationClip close;
        [SerializeField] private AudioVariation moveAudio;
        //[HideInInspector]
        [CanBeNull] public Wire wire;
        private SpriteRenderer doorBeam;
        public bool inverted;
        [SerializeField]private bool m_Open = false, opening = false, closing = false;
        private BoxCollider2D m_DoorCollider;
        private Animator m_DoorAnimator;
        private AudioSource m_AudioSource;
        private bool wasActive;
        private Light2D light2D;
        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            light2D = GetComponentInChildren<Light2D>();
            m_DoorCollider = GetComponent<BoxCollider2D>();
            m_DoorAnimator = GetComponent<Animator>();
            m_AudioSource = GetComponent<AudioSource>();
            if (transform.childCount > 0)
                doorBeam = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        private void Start() {
            if (inverted) {
                Opened();
                LightOff();
            } else
                LightOn();
            UpdateMaterial();
        }
        private void Update() {
            Open();
        }
        private void Open(){
            if (wire == null) {
                throw new WarningException(name + " is not connected by a wire, please connect it");
            }
            if (opening || closing) return;
            if (wire.active && !inverted && !wasActive || !wire.active && inverted && wasActive) {
                opening = true;
                Opened();
            } else if (!wire.active && !inverted && wasActive || wire.active && inverted && !wasActive) {
                closing = true;
                Closed();
            }
            wasActive = wire.active;
        }
        private void Opened() {
            //todo Sound and Particles
            print("opened");
            moveAudio.PlayAudio(m_AudioSource);
            m_DoorAnimator.Play(open.name);
        }
        private void Closed() {
            //todo Sound and Particles
            print("closed");
            moveAudio.PlayAudio(m_AudioSource);
            m_DoorAnimator.Play(close.name);
            m_DoorCollider.enabled = true;
            doorBeam.enabled = false;
        }
        public void TurnOffCollider() {
            if (wire.active && inverted || !wire.active && !inverted) return;
            m_DoorCollider.enabled = false;
            doorBeam.enabled = true;
        }
        void UpdateMaterial() {
            spriteRenderer.material.SetFloat("_Active",1f);
        }
        public void LightOff() {
            light2D.enabled = false;
        }
        public void LightOn() {
            light2D.enabled = true;
        }
        public void FinishedOpening() {
            opening = false;
            TurnOffCollider();
        }
        public void FinishedClosing() {
            closing = false;
        }
    }
}