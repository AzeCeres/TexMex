using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
namespace Puzzle {
    public class Wire : MonoBehaviour {
        [HideInInspector][CanBeNull] public Button button;
        [CanBeNull] private WireConnector m_WireConnector;
        
        public bool active;
        private readonly List<GameObject> m_PuzzleObjects = new List<GameObject>(), m_Buttons = new List<GameObject>();
        
        [CanBeNull] private Light2D light2D;
        private TilemapRenderer wiRenderer;
        private void Awake() {
            wiRenderer = GetComponent<TilemapRenderer>();
            light2D = GetComponentInChildren<Light2D>();
        }
        void UpdateMaterial() {
            wiRenderer.material.SetFloat("_Active", active ? 1f : 0f);
        }
        void UpdateLight() {
            light2D.enabled = active;
        }
        private void Start() {
            GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (var go in gameObjects) {
                if (go.CompareTag("Puzzle")) {
                    m_PuzzleObjects.Add(go);
                }
                else if (go.CompareTag("Button")) {     
                    m_Buttons.Add(go);
                }
            }
            UpdateMaterial();
            if (light2D != null) {
                UpdateLight();
            }
        }   
        private void Update() { 
            UpdateMaterial();
            if (light2D != null) {
                UpdateLight();
            }
            if (ActivityCheck())
                Active();
            else
                InActive();
        }
        // todo // Get Color from either button or button connector
        void Active() {
            //todo Sound and Particles
        }
        void InActive() {
            //todo powerDown sound
        }
        private bool ActivityCheck() {
            active = false;
            if (button == null && m_WireConnector == null) {
                throw new WarningException(name + " Wire is not connected to either button or Connector");
            }
            if (button == null) {
                active = m_WireConnector.active;
                return active;
            }
            active = button.active;
            return active;    
        }
        private void OnTriggerEnter2D(Collider2D other) {
            //print(other.gameObject.name); //for testing, in case of objects not being seen. Make sure they are Kinematic, as static objects aren't seen apparently
            for (int i = 0; i < m_Buttons.Count; i++) {
                if (other.gameObject != m_Buttons[i])
                    continue;
                button = other.gameObject.GetComponent<Button>();
                //buttons.Remove(other.gameObject);
            }
            for (int i = 0; i < m_PuzzleObjects.Count; i++) {
                if (other.gameObject != m_PuzzleObjects[i])
                    continue;
                if (other.gameObject.TryGetComponent(out Door door)) {
                    door.wire = this; //Not working? or not entering eachothers triggers
                } if (other.gameObject.TryGetComponent(out LaserShooter laser)) {
                    laser.wire = this;
                } if (other.gameObject.TryGetComponent(out WireConnector wireCon))
                {
                    m_WireConnector = wireCon;
                    wireCon.wires.Add(this);
                }
            }
        }
    }
}
