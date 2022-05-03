using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class PotBreaking : MonoBehaviour
{
    private Animator animator;
    private AudioSource audio;
    [SerializeField] private AudioVariation potBreaking;
    private bool broken = false;
    private SpriteRenderer renderer;
    [SerializeField] private Sprite pot_broken;


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
                potBreaking.PlayAudio(audio);
            }
            broken = true;
        }
    }

   private void SpriteBroken()
   {
       renderer.sprite = pot_broken;
   }
}
