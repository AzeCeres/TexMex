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
    private SpriteRenderer renderer;
    [SerializeField] private Sprite pot_broken;
    
    [SerializeField] [Range(0f, 1f)] private float pitchLowerBound = 1f;
    [SerializeField] [Range(1f, 2f)] private float pitchUpperBound = 1f;
    

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (broken == false);
        {
            SpriteBroken();
            
            if (broken == false)
            {
                var pitch = Random.Range(pitchLowerBound, pitchUpperBound);
                audio.pitch = pitch;
                audio.PlayOneShot(potBreaking);
                audio.pitch = 1f;
            }
            broken = true;
        }
    }

   private void SpriteBroken()
   {
       renderer.sprite = pot_broken;
   }
}
