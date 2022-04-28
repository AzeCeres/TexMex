using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
namespace Puzzle
{
    public class Wire : MonoBehaviour
    {
        [HideInInspector][CanBeNull] public Button button;
        //[SerializeField][CanBeNull] private Wire[] wire;
        [CanBeNull] private WireConnector wireConnector;
        public bool active;
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
        // todo // Get Color from either button or button connector
        void Active() {
        }
        void InActive() {
        }
        private bool ActivityCheck() {
            active = false;
            if (button == null && wireConnector == null) {
                throw new WarningException(name + " Wire is not connected to either button or Connector");
            }
            if (button == null) {
                active = wireConnector.active;
                return active;
            }
            active = button.active;
            return active;    
        }
        private void OnTriggerEnter2D(Collider2D other) {
            //print(other.gameObject.name); //for testing, in case of objects not being seen. Make sure they are Kinematic, as static objects aren't seen apparently
            for (int i = 0; i < buttons.Count; i++) {
                if (other.gameObject != buttons[i])
                    continue;
                button = other.gameObject.GetComponent<Button>();
                //buttons.Remove(other.gameObject);
            }
            for (int i = 0; i < puzzleObjects.Count; i++) {
                if (other.gameObject != puzzleObjects[i])
                    continue;
                if (other.gameObject.TryGetComponent(out Door door)) {
                    door.wire = this; //Not working? or not entering eachothers triggers
                } if (other.gameObject.TryGetComponent(out LaserShooter laser)) {
                    laser.wire = this;
                } if (other.gameObject.TryGetComponent(out WireConnector wireCon))
                {
                    wireConnector = wireCon;
                    wireCon.wires.Add(this);
                }
            }
        }
    }
}
