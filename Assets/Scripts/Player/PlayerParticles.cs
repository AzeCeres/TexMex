using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticleSystem;
    [SerializeField] private ParticleSystem deathParticles;

    public void PlayWalkParticle()
    {
        walkParticleSystem.Play();
    }
    public void PlayDeathParticles()
    {
        deathParticles.Play();
    }
}
