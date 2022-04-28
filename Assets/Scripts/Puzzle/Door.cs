using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;
using Component = UnityEngine.Component;

namespace Puzzle {
    public class Door : MonoBehaviour {
        //[HideInInspector]
        [CanBeNull] public Wire wire;
        public bool inverted;
        private bool m_Open;
        private Component m_DoorCollider;
        private Component m_DoorRenderer;
        private void Awake() {
            if (TryGetComponent(out TilemapCollider2D tilemap)) {
                m_DoorCollider = tilemap;
            }else if (TryGetComponent(out BoxCollider2D boxCollider)) {
                m_DoorCollider = boxCollider;
            }
            if (TryGetComponent(out TilemapRenderer tmRenderer)){
                m_DoorRenderer = tmRenderer;
            }
            else if (TryGetComponent(out SpriteRenderer sRenderer)) {
                m_DoorRenderer = sRenderer;
            }
        }
        private void Update() {
            Open();
        }
        private void Open(){
            if (wire == null) {
                throw new WarningException(name + " is not connected by a wire, please connect it");
            } if (wire.active && !inverted || !wire.active && inverted) {
                m_DoorRenderer.gameObject.SetActive(false);
                if (m_DoorCollider == null) return;
                m_DoorCollider.gameObject.SetActive(false);
            } else { 
                m_DoorRenderer.gameObject.SetActive(true);
                m_DoorCollider.gameObject.SetActive(true);
            } 
               
        }
    }
}
