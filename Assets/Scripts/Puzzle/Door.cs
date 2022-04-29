using System;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

namespace Puzzle {
    public class Door : MonoBehaviour {
        [SerializeField] private AnimationClip open;
        [SerializeField] private AnimationClip close;
        [HideInInspector][CanBeNull] public Wire wire;
        [CanBeNull] private SpriteRenderer doorBeam;
        public bool inverted;
        private bool m_Open;
        private BoxCollider2D m_DoorCollider;
        private Animator m_DoorAnimator;
        private bool wasActive;
        private void Awake() {
            m_DoorCollider = GetComponent<BoxCollider2D>();
            m_DoorAnimator = GetComponent<Animator>();
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
        private void Open(){
            if (wire == null) {
                throw new WarningException(name + " is not connected by a wire, please connect it");
            } if (wire.active && !inverted && !wasActive || !wire.active && inverted && wasActive) {
                Opened();
                print("DeActivated");
            } else if (!wire.active && !inverted && wasActive || wire.active && inverted && !wasActive){ 
                Closed();
                print("Closed");
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
            //todo Sound and Particles
            m_DoorAnimator.Play(close.name); 
            m_DoorCollider.enabled = true;
            if (doorBeam == null) return;
            doorBeam.enabled = false;
        }
        public void TurnOffCollider() {
            m_DoorCollider.enabled = false;
        }
    }
}
