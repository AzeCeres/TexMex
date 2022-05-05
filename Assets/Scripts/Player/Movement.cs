using Player;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Split _mSplit;
    [SerializeField] private float moveSpeed = 5f;
    private float _movementMultiplier = 10f;
    
    [SerializeField]private float drag = 5.5f;
    //[SerializeField]private Rigidbody2D mainRB, secondRB;
    private void Awake() {
        _mSplit = GetComponent<Split>();
    }
    private void FixedUpdate() {
        Drag();
    }
    private void Drag() {
        for (int i = 0; i < _mSplit.clones.Length; i++) {
            if (_mSplit.activeClones[i]) {
                //todo make it only  GetComponent once
                var rb = _mSplit.clones[i].GetComponent<Rigidbody2D>();
                
                Vector2 velocity = transform.InverseTransformDirection(rb.velocity);
                float forceX = -drag * velocity.x;
                float forceZ = -drag * velocity.y;
        
                rb.AddRelativeForce(new Vector2(forceX, forceZ));
            }
        }
    }
    
    public void MoveMain(Vector2 moveVector) {
        if (_mSplit.mainClones.Count == 0) return;
        //todo make it only happen once
        var rb = _mSplit.mainClones[_mSplit.selectedMain].GetComponent<Rigidbody2D>();
        rb.AddForce(moveVector.normalized * (moveSpeed * _movementMultiplier), ForceMode2D.Force);
    }
    public void MoveSecond(Vector2 moveVector) {
        //todo make it only happen once
        if (_mSplit.secondClones.Count == 0) return;
        var rb = _mSplit.secondClones[_mSplit.selectedSecond].GetComponent<Rigidbody2D>();
        rb.AddForce(moveVector.normalized * (moveSpeed * _movementMultiplier), ForceMode2D.Force);
    }
}
