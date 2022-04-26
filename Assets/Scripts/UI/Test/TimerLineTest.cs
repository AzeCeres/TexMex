using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimerLineTest : MonoBehaviour
{
    private Animator _testAnimator;
    public GameObject[] lines;
    // Start is called before the first frame update
    void Start()
    {
        _testAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            _testAnimator.Play("TimerLineTest");
        }
    }
    //put all lines in array or list and chain them that way
    //Can we hide this with an object on each corner ??????
    public void PlayNextAnimation()
    {
        lines[1].GetComponent<Animator>().Play("TimerLineUpward");
    }
}
