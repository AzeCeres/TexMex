using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMove : MonoBehaviour
{
    [SerializeField] private float creditsMoveSpeed = 2f;
    
    
    void FixedUpdate()
        {
            Vector3 creditsVelocity = Vector3.up * creditsMoveSpeed;
                    transform.Translate(creditsVelocity * Time.deltaTime);
        }
}
