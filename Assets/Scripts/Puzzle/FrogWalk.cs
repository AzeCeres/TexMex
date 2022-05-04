using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class FrogWalk : MonoBehaviour
{
    [SerializeField] private float frogSpeed = 5f;
    [SerializeField] private float rotateSpeed = 20f;
    [SerializeField] private int behaviour;
    [SerializeField] private float timer;

    private Rigidbody2D RB;


    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        behaviour = (Random.Range(1, 8));
    }

    void Update()
    {
        if (behaviour == 1)
        {
            RB.velocity = Vector2.right * frogSpeed;
            transform.Rotate(0, 0, 0);
            transform.localScale = new Vector2(1, 1);
        }
        
        if (behaviour == 2)
        {
            RB.velocity = Vector2.left * frogSpeed;
            transform.Rotate(0, 0, 0);
            transform.localScale = new Vector2( -1, 1);
        }
        
        if (behaviour == 3)
        {
            RB.velocity = Vector2.up * frogSpeed;
            //transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime,Space.World);
        }
        
        if (behaviour == 4)
        {
            RB.velocity = Vector2.down * frogSpeed;
            //transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime,Space.World);
        }
        
        if (behaviour == 5)
        {
            RB.velocity = Vector2.up + Vector2.right* frogSpeed;
            transform.localScale = new Vector2(1, 1);
        }
        
        if (behaviour == 6)
        {
            RB.velocity = Vector2.up + Vector2.left* frogSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        
        if (behaviour == 7)
        {
            RB.velocity = Vector2.down + Vector2.right* frogSpeed;
            transform.localScale = new Vector2(1, 1);
        }

        if (behaviour == 8)
        {
            RB.velocity = Vector2.down + Vector2.left* frogSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        
        

        Flip();
        
        {
            //transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime,Space.World);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        behaviour = (Random.Range(1, 4));
    }

    private void Flip()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            behaviour = (Random.Range(1, 8));
            timer = 0f;
        }
    }
}
