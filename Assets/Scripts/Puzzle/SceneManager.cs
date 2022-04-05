using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Player;
using Puzzle;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //todo Check if all clones are dead - Reset all Buttons
    [SerializeField] private Button[] Buttons;
    private GameObject startPos;
    private Split m_Split;
    private void Start() {
        var obj = GameObject.FindGameObjectWithTag("PlayerController");
        m_Split = obj.GetComponent<Split>();
        startPos = obj;
        print(name);
    }
    private void Update()
    {
        CheckPlayer();
    }
    void CheckPlayer()
    {
        var count = 0;
        for (int i = 0; i < m_Split.activeClones.Length; i++)
        {
            if (!m_Split.activeClones[i])
            {
                count++;
                print(count);
            }
            if (count == m_Split.activeClones.Length)
            {
                Reset();
            }
        }
    }
    private void Reset()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].active = false;
        }
        
        m_Split.SpawnClone(0, m_Split.mainClones, startPos);
    }
}
