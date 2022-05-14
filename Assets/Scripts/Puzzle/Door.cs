using System.ComponentModel;
using Audio;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Puzzle {
    public class Door : MonoBehaviour {
        [SerializeField] private AnimationClip open;
        [SerializeField] private AnimationClip close;
        //[HideInInspector]
        [CanBeNull] public Wire wire;
        private SpriteRenderer doorBeam;
        public bool inverted;
        public bool _noAudioFirstOpen = false;
        [SerializeField]private bool m_Open = false, opening = false, closing = false;
        private BoxCollider2D _doorCollider;
        private Animator _doorAnimator;
        private bool _wasActive;
        [CanBeNull] private Light2D _light2D;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem openParticles, closeParticles, openingParticles, closingParticles;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _light2D = GetComponentInChildren<Light2D>();
            _doorCollider = GetComponent<BoxCollider2D>();
            _doorAnimator = GetComponent<Animator>();
            GetComponent<AudioSource>();
            if (transform.childCount > 0)
                doorBeam = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _noAudioFirstOpen = inverted;
        }
        private void Start() {
            if (inverted) {
                Opened();
                LightOff();
            } else
                LightOn();
            UpdateMaterial();
        }
        private void Update() => Open();
        private void Open(){
            if (wire == null) {
                throw new WarningException(name + " is not connected by a wire, please connect it");
            }
            if (opening || closing) return;
            if (wire.active && !inverted && !_wasActive || !wire.active && inverted && _wasActive) {
                opening = true;
                Opened();
            } else if (!wire.active && !inverted && _wasActive || wire.active && inverted && !_wasActive) {
                closing = true;
                Closed();
            }
            _wasActive = wire.active;
        }
        private void Opened() {
            //todo Sound and Particles
            _doorAnimator.Play(open.name);
            if (_doorCollider == null) return;
            Invoke(nameof(TurnOffCollider), 0.9f);
            if (doorBeam == null) return;
            doorBeam.enabled = true;
        }
        private void Closed() {
            //todo Sound and Particles
            _doorAnimator.Play(close.name);
            _doorCollider.enabled = true;
            doorBeam.enabled = false;
        }
        public void TurnOffCollider() {
            if (wire.active && inverted || !wire.active && !inverted) return;
                _doorCollider.enabled = false;
                doorBeam.enabled = true;
        }
        void UpdateMaterial() => _spriteRenderer.material.SetFloat("_Active",1f);
        public void LightOff() => _light2D.enabled = false;
        public void LightOn() => _light2D.enabled = true;
        public void FinishedOpening() {
            opening = false;
            TurnOffCollider();
        }
        public void FinishedClosing() => closing = false;
        public void PlayOpeningParticles() => openingParticles.Play();
        public void PlayClosingParticles() => closingParticles.Play();
        public void PlayCloseParticles() {
            closeParticles.Play();
            closingParticles.Stop();
        }
        public void PlayOpenParticles() {
            openParticles.Play();
            openingParticles.Stop();
        }
        private void PlayDoorOpenAudio() {
            if (!_noAudioFirstOpen) {
                DoorAudioController.PlayDoorAudio(true);
            }
            _noAudioFirstOpen = false;
        }

        private void PlayDoorCloseAudio()
        {
            DoorAudioController.PlayDoorAudio(false);
        }
    }
}