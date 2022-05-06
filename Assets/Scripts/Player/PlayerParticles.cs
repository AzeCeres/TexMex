using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticleSystem;


    public void PlayWalkParticle()
    {
        walkParticleSystem.Play();
    }
}
