using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

namespace Puzzle {
    public class Door : MonoBehaviour {
        [SerializeField] private Sprite activeDoor;
        [SerializeField] private Sprite deActivatedDoor;
        [SerializeField] private Sprite openedDoor;
        [HideInInspector][CanBeNull] public Wire wire;
        public bool inverted;
        private bool m_Open;
        private BoxCollider2D m_DoorCollider;
        private SpriteRenderer m_DoorRenderer;
        private bool wasActive;
        private void Awake() {
            m_DoorCollider = GetComponent<BoxCollider2D>();
            m_DoorRenderer = GetComponent<SpriteRenderer>();
        }
        private void Update() {
            Open();
        }
        private void Open(){
            if (wire == null) {
                throw new WarningException(name + " is not connected by a wire, please connect it");
            } if (wire.active && !inverted && !wasActive|| !wire.active && inverted && wasActive) {
                DeActivated();
                print("DeActivated");
            } else if (!wire.active && !inverted && wasActive|| wire.active && inverted && !wasActive){ 
                Closed();
                print("Closed");
            }
            wasActive = wire.active;
        }
        private void DeActivated() {
            //todo Sound and Particles
            m_DoorRenderer.sprite = deActivatedDoor;
            Invoke("Opened", 0.15f);
        }
        private void Opened() {
            //todo Sound and Particles
            m_DoorRenderer.sprite = openedDoor;
            if (m_DoorCollider == null) return;
            m_DoorCollider.enabled = false;
        }
        private void Closed()
        {
            //todo Sound and Particles
            m_DoorRenderer.sprite = activeDoor;
            m_DoorCollider.enabled = true;
        }
    }
}
