using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class PotBreaking : MonoBehaviour
{
    private Animator animator;
    private AudioSource audio;
    [SerializeField] private AudioClip potBreaking;
    private bool broken = false;
    [SerializeField] private bool breaking = false;
    

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
            animator.Play("Pot_broken");
            audio.PlayOneShot(potBreaking);
            broken = true;
            breaking = false;
        }
        
    }
}
