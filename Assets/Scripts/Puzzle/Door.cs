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
        private bool m_Open;
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
        private void Update() {
            Open();
        }
        private void Open(){
            if (wire == null) {
                throw new WarningException(name + " is not connected by a wire, please connect it");
            } if (wire.active && !inverted && !wasActive|| !wire.active && inverted && wasActive) {
                Opened();
                print("DeActivated");
            } else if (!wire.active && !inverted && wasActive|| wire.active && inverted && !wasActive){
                Closed();
                print("Closed");
            }
            wasActive = wire.active;
        }
        private void Opened() {
            //todo Sound and Particles
            moveAudio.PlayAudio(m_AudioSource);
            m_DoorAnimator.Play(open.name);
            if (m_DoorCollider == null) return;
            m_DoorCollider.enabled = false;
            if (doorBeam == null) return;
            doorBeam.enabled = true;
        }
        private void Closed() {
            //todo Sound and Particles
            moveAudio.PlayAudio(m_AudioSource);
            m_DoorAnimator.Play(close.name);
            m_DoorCollider.enabled = true;
            if (doorBeam == null) return;
            doorBeam.enabled = false;
        }
    }
}