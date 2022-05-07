using JetBrains.Annotations;
using Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Puzzle {
    public class LaserShooter : MonoBehaviour {
        // private PlayerAnimator[] cloneAnimators;
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
        [SerializeField] private bool up;

        private Light2D Light2D;
        private void Awake() {
            Light2D = GetComponentInChildren<Light2D>();
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
            if (up && Physics2D.Raycast(transform.position, transform.up)){
                RaycastHit2D hit = Physics2D.Raycast(startPoint.position, transform.up, whatToHit);
                if (hit.transform.gameObject.layer == 6) {
                    var animator = hit.transform.gameObject.GetComponent<PlayerAnimator>();
                    animator.Death();
                }   
                DrawRay(startPoint.position, hit.point); 
                
            }else if(up) {
                DrawRay(startPoint.position, startPoint.transform.up * maxDistance);

            }
            else if(!up && Physics2D.Raycast(transform.position, transform.right)) {
                RaycastHit2D hit = Physics2D.Raycast(startPoint.position, transform.right, whatToHit);
                if (hit.transform.gameObject.layer == 6) {
                    var animator = hit.transform.gameObject.GetComponent<PlayerAnimator>();
                    animator.Death();
                }
                
                DrawRay(startPoint.position, hit.point); 
            } else if(!up) {
                DrawRay(startPoint.position, startPoint.transform.right * maxDistance);
            }
        }
        private void DrawRay(Vector2 startPos, Vector2 endPos) {
            var distance = Vector2.Distance(startPos, endPos);
            if (up && startPos.y < endPos.y) {
                lineRenderer.SetPosition(0, startPos);
                lineRenderer.SetPosition(1, endPos + new Vector2(0, 0.5f));
                Light2D.pointLightOuterRadius = distance+1f;
            }else {
                lineRenderer.SetPosition(0, startPos);
                lineRenderer.SetPosition(1, endPos);
                Light2D.pointLightOuterRadius = distance+0.5f;
            }
            
            
            Light2D.enabled = true;
        }
        private void DontDrawRay() {
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, startPoint.position);
            Light2D.enabled = false;
        }
    }
}
