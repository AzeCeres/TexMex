using System;
using Puzzle;
using UnityEngine;

public class MovingChamber : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField][Range(1,2)] private Transform[] targets;
    [SerializeField] private float moveSpeed;
    
    private void FixedUpdate() {
        if(button.active)
            MoveToTarget();
        else
            MoveToStart();
    }
    void MoveToTarget() {
        //moves to the next point in the array when its active
        transform.position =Vector3.MoveTowards(transform.position, targets[1].position, moveSpeed);
    }
    void MoveToStart() {
        transform.position =Vector3.MoveTowards(transform.position, targets[0].position, moveSpeed);
    }
    private void OnTriggerEnter(Collider other) {
        // move the player with the chamber at the same speed as it. player has to be able to move normally when in it
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.transform.parent = transform;
        }
    }
    // private void OnTriggerExit(Collider other) {
    //     throw new NotImplementedException();
    //     if (other.gameObject.CompareTag("Player")) {
    //         other.gameObject.transform.parent = transform;
    //     }
    // }
}
