using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class Frogpot : MonoBehaviour
{
    private Animator animator;
    private AudioSource audio;
    [SerializeField] private AudioVariation potBreaking;
    private bool broken = false;
    [SerializeField] private GameObject forg;
   


    private void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (broken == false);
        {
            animator.Play("ForgPot_broKen");
            
            
            if (broken == false)
            {
                potBreaking.PlayAudio(audio);
                Instantiate(forg, transform.position, Quaternion.identity);
            }
            broken = true;
        }
    }
}
