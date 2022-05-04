using System;
using System.ComponentModel;
using Audio;
using JetBrains.Annotations;
using UnityEngine;

namespace Puzzle {
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour {
        [SerializeField] private AnimationClip open;
        [SerializeField] private AnimationClip close;
        [SerializeField] private AudioVariation moveAudio;
        [HideInInspector][CanBeNull] public Wire wire;
        [CanBeNull] private SpriteRenderer doorBeam;
        public bool inverted;
        private bool m_Open = false, opening = false, closing = false;
        private BoxCollider2D m_DoorCollider;
        private Animator m_DoorAnimator;
        private AudioSource m_AudioSource;
        private bool wasActive;

        private void Awake() {
            m_DoorCollider = GetComponent<BoxCollider2D>();
            m_DoorAnimator = GetComponent<Animator>();
            m_AudioSource = GetComponent<AudioSource>();
            if (transform.childCount > 0)
                doorBeam = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        private void Start() {
            if (inverted) {
                Opened();
            }
        }
        private void Update() {
            Open();
        }
        private void Open()
        {
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
            m_DoorAnimator.Play(open.name);
            if (m_DoorCollider == null) return;
            Invoke(nameof(TurnOffCollider), 0.9f);
            if (doorBeam == null) return;
            doorBeam.enabled = true;
        }
        private void Closed() {
            print("Attempting close");
            //todo Sound and Particles
            m_DoorAnimator.Play(close.name);
            m_DoorCollider.enabled = true;
            if (doorBeam == null) return;
            doorBeam.enabled = false;
        }
        public void TurnOffCollider() {
            if (wire.active && inverted || !wire.active && !inverted) return;
                m_DoorCollider.enabled = false;
        }

        private void PlayDoorMoveAnimation() {
            moveAudio.PlayAudio(m_AudioSource);
        }
        public void FinishedOpening() {
            opening = false;
        }
        public void FinishedClosing() {
            closing = false;
        }
    }
}