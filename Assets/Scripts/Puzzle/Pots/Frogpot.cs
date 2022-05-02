using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frogpot : MonoBehaviour
{
    private Animator animator;
    private bool broken = false;
    [SerializeField] private bool breaking = false;
    [SerializeField] private GameObject forg;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (broken = false);
        {
            breaking = true;
        }
        if (breaking)
        {
            animator.Play("ForgPot_broKen");
            broken = true;
            Instantiate(forg, new Vector3(0, 0, 0), Quaternion.identity);
            breaking = false;
        }
        
    }

    
    
}
