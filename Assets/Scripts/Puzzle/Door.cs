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
            m_DoorCollider = GetComponentInChildren<BoxCollider2D>();
            m_DoorRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        private void Update() {
            Open();
        }
        private void Open(){
            if (wire == null) {
                print(name + " is not connected by a wire, please connect it");
                return;
            }
            if (wire.active && !inverted || !wire.active && inverted) {
                m_DoorRenderer.enabled = false;
                if (m_DoorCollider == null) return;
                m_DoorCollider.enabled = false;
            }
            else //if (m_Open && inverted || m_Open && inverted){
                m_DoorRenderer.enabled = true;
            if (m_DoorCollider == null) return;
                m_DoorCollider.enabled = true;
        }
    }
}
