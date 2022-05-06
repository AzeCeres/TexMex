using UnityEngine;
namespace Player {
    public class Movement : MonoBehaviour {
        private Split m_Split;
        [SerializeField] private float moveSpeed = 5f;
        private float m_MovementMultiplier = 10f;
        [SerializeField]private Rigidbody2D[] m_Rb;
        [SerializeField]private float drag = 5.5f;
        //[SerializeField]private Rigidbody2D mainRB, secondRB;
        private void Awake() {
            m_Split = GetComponent<Split>();
        }
        // private void Start() {
        //     for (var i = 0; i < m_Split.clones.Length; i++) {
        //         var clone = m_Split.clones[i];
        //         m_Rb[i] = clone.GetComponent<Rigidbody2D>();
        //     }
        // }
        private void FixedUpdate() {
            Drag();
        }
        private void Drag() {
            for (int i = 0; i < m_Split.clones.Length; i++) {
                if (m_Split.activeClones[i]) {
                    Vector2 velocity = transform.InverseTransformDirection(m_Rb[i].velocity);
                    float forceX = -drag * velocity.x;
                    float forceZ = -drag * velocity.y;
                    m_Rb[i].AddRelativeForce(new Vector2(forceX, forceZ));
                } else {
                    Vector2 velocity = transform.InverseTransformDirection(m_Rb[i].velocity);
                    var deathDrag = drag / 2;
                    float forceX = -deathDrag * velocity.x;
                    float forceZ = -deathDrag * velocity.y;
                    m_Rb[i].AddRelativeForce(new Vector2(forceX, forceZ));
                }
            }
        }
        public void MoveMain(Vector2 moveVector) {
            if (m_Split.mainClones.Count == 0) return;
            //todo make it only happen once
            var rb = m_Split.mainClones[m_Split.selectedMain].GetComponent<Rigidbody2D>();
            rb.AddForce(moveVector.normalized * (moveSpeed * m_MovementMultiplier), ForceMode2D.Force);
        }
        public void MoveSecond(Vector2 moveVector) {
            //todo make it only happen once
            if (m_Split.secondClones.Count == 0) return;
            var rb = m_Split.secondClones[m_Split.selectedSecond].GetComponent<Rigidbody2D>();
            rb.AddForce(moveVector.normalized * (moveSpeed * m_MovementMultiplier), ForceMode2D.Force);
        }
    }
}
