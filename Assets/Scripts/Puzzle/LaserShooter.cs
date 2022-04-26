using JetBrains.Annotations;
using Player;
using UnityEngine;
namespace Puzzle {
    public class LaserShooter : MonoBehaviour {
        private Split m_Split;
        [SerializeField] private float maxDistance = 100f;
        [SerializeField] private Transform startPoint;
        [SerializeField] private LineRenderer lineRenderer;
        [Tooltip("Makes it so that the laser is always firing so long as the bool is true")]
        public bool alwaysActive = false;
        [Tooltip("Only matters if theres a button attached, reverses the button state, (Is on when the button is off, and off when button is on)")]
        public bool inverted = false;
        [Tooltip("Button can be left as empty, the shooter will then be considered always active. otherwise, if a button is connected the laser will only fire when a button is pressed, or opposite when its inverted")]
        [HideInInspector] [CanBeNull]public Wire wire;
        [SerializeField] private LayerMask whatToHit, playerLayer;
        private void Awake() {
            var gameObjects = FindObjectsOfType<GameObject>();
            foreach (var t in gameObjects) {
                if (!t.CompareTag("PlayerController"))
                    continue;
                if (t.TryGetComponent(typeof(Split), out Component component)) {
                    m_Split = t.GetComponent<Split>();
                }   
            }
        }
        private void Update() {
            if (wire == null) {
                FireLaser();
                return;
            } if (alwaysActive || wire.active && !inverted || !wire.active && inverted) {
                FireLaser();
            } else {
                DontDrawRay();
            }
        }
        private void FireLaser() {
            if (Physics2D.Raycast(transform.position, transform.right)) {
                RaycastHit2D hit = Physics2D.Raycast(startPoint.position, transform.right, whatToHit);
                if (hit.transform.gameObject.layer == 6) { 
                    m_Split.KillClone(hit.transform.gameObject);
                }   
                DrawRay(startPoint.position, hit.point); 
            } else {
                DrawRay(startPoint.position, startPoint.transform.right * maxDistance);
            }
        }
        private void DrawRay(Vector2 startPos, Vector2 endPos) {
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
        }
        private void DontDrawRay() {
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, startPoint.position);
        }
    }
}
