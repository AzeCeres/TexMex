using Player;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Split m_Split;
    [SerializeField] private float moveSpeed = 5f;
    private float movementMultiplier = 10f;
    
    [SerializeField]private float drag = 5.5f;
    //[SerializeField]private Rigidbody2D mainRB, secondRB;
    private void Awake() {
        m_Split = GetComponent<Split>();
    }
    private void FixedUpdate() {
        Drag();
    }
    private void Drag() {
        for (int i = 0; i < m_Split.clones.Length; i++) {
            if (m_Split.activeClones[i]) {
                //todo make it only  GetComponent once
                var rb = m_Split.clones[i].GetComponent<Rigidbody2D>();
                
                Vector2 velocity = transform.InverseTransformDirection(rb.velocity);
                float forceX = -drag * velocity.x;
                float forceZ = -drag * velocity.y;
        
                rb.AddRelativeForce(new Vector2(forceX, forceZ));
            }
        }
    }
    
    public void MoveMain(Vector2 moveVector) {
        if (m_Split.mainClones.Count == 0) return;
        //todo make it only happen once
        var rb = m_Split.mainClones[m_Split.selectedMain].GetComponent<Rigidbody2D>();
        rb.AddForce(moveVector.normalized * (moveSpeed * movementMultiplier), ForceMode2D.Force);
    }
    public void MoveSecond(Vector2 moveVector) {
        //todo make it only happen once
        if (m_Split.secondClones.Count == 0) return;
        var rb = m_Split.secondClones[m_Split.selectedSecond].GetComponent<Rigidbody2D>();
        rb.AddForce(moveVector.normalized * (moveSpeed * movementMultiplier), ForceMode2D.Force);
    }
}
