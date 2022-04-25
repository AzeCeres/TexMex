using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
namespace Puzzle
{
    public class Wire : MonoBehaviour
    {
        [SerializeField][CanBeNull] private Button button;
        [SerializeField][CanBeNull] private Wire[] wire;
        public bool active;
        // private List<GameObject> m_WireSegments;
        // [SerializeField] private Color inActiveColor;
        // [CanBeNull] public Color color;
        
        private List<GameObject> puzzleObjects = new List<GameObject>(), buttons = new List<GameObject>();
        private void Start() {
            GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (var go in gameObjects) {
                if (go.CompareTag("Puzzle")) {
                    puzzleObjects.Add(go);
                }
                else if (go.CompareTag("Button")) {     
                    buttons.Add(go);
                }
            }
        }   
        private void Update() {
            if (ActivityCheck())
                Active();
            else
                InActive();
        }
        // todo // Dont get spriteRenderer every frame
        void Active() {
            if (!active)
                return;
            // foreach (var go in m_WireSegments) {
            //     var spriteRenderer = go.GetComponent<SpriteRenderer>();
            //     spriteRenderer.color = color;
            // }
        }
        void InActive() {
            // foreach (var go in m_WireSegments) { 
            //     var spriteRenderer = go.GetComponent<SpriteRenderer>();
            //     spriteRenderer.color = inActiveColor;
            // }
        }
        private bool ActivityCheck() {
            active = false;
            // if (wire != null) {
            //     var activeWires = 0;
            //     foreach (var w in wire) {
            //         if (w.active) 
            //             activeWires++;  
            //         if (activeWires == wire.Length) {
            //             active = true;
            //             //color = w.color;
            //         }
            //     }
            // } 
            if (button == null)
                return active;
            active = button.active;
            return active;    
        }
        private void OnTriggerEnter2D(Collider2D other) {
            for (int i = 0; i < buttons.Count; i++) {
                if (other.gameObject != buttons[i])
                    continue;
                button = other.gameObject.GetComponent<Button>();
                //buttons.Remove(other.gameObject);
            }
            for (int i = 0; i < puzzleObjects.Count; i++) {
                if (other.gameObject != puzzleObjects[i])
                    continue;
                if (other.gameObject.transform.parent.TryGetComponent(out Door door)) {
                    door.wire = gameObject.GetComponent<Wire>();
                } if (other.gameObject.TryGetComponent(out LaserShooter laser)) {
                    laser.wire = gameObject.GetComponent<Wire>();
                }
            }
        }
    }
}
