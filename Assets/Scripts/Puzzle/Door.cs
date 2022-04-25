using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
namespace Puzzle
{
    public class Door : MonoBehaviour
    {
        //[HideInInspector]
        [CanBeNull] public Wire wire;
        public bool inverted;
        private bool m_Open;
        private BoxCollider2D m_DoorCollider;
        private SpriteRenderer m_DoorRenderer;
        private void Awake() {
            m_DoorCollider = GetComponent<BoxCollider2D>();
            m_DoorRenderer = GetComponent<SpriteRenderer>();
            print(m_DoorCollider);
        }
        private void Update() {
            Open();
        }
        private void Open(){
            if (wire == null) {
                throw new WarningException(name + " is not connected by a wire, please connect it");
            } if (wire.active && !inverted || !wire.active && inverted) {
                m_DoorRenderer.enabled = false;
                if (m_DoorCollider == null) return;
                m_DoorCollider.enabled = false;
            } else { 
                m_DoorRenderer.enabled = true;
                m_DoorCollider.enabled = true;
            } 
               
        }
    }
}
