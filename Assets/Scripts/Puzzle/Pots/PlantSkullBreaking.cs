using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class PlantSkullBreaking : MonoBehaviour
{
    private Animator animator;
    private AudioSource audio;
    [SerializeField] private AudioVariation potBreaking;
    private bool broken = false;
   


    private void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (broken == false);
        {
            animator.Play("Skull_Grass_Broken");
            
            if (broken == false)
            {
                potBreaking.PlayAudio(audio);
            }
            broken = true;
        }
    }
}
