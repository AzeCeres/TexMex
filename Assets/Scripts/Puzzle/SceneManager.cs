using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Player;
using Puzzle;
using UnityEngine;
using UnityEngine.Serialization;

public class SceneManager : MonoBehaviour
{
    //todo Check if all clones are dead - Reset all Buttons
    [FormerlySerializedAs("Buttons")] [SerializeField] private Button[] buttons;
    private GameObject _startPos;
    private Split _mSplit;
    private void Start() {
        var obj = GameObject.FindGameObjectWithTag("PlayerController");
        _mSplit = obj.GetComponent<Split>();
        _startPos = obj;
        print(name);
    }
    private void Update()
    {
        print("is running");
        CheckPlayer();
    }
    void CheckPlayer()
    {
        var count = 0;
        print("checkplayer is running");
        for (int i = 0; i < _mSplit.activeClones.Length; i++)
        {
            if (!_mSplit.activeClones[i])
            {
                count++;
                print(count);
            }
            if (count == _mSplit.activeClones.Length)
            {
                print("reset");
                Reset();
            }
        }
    }
    private void Reset()
    {
        print("reset");
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].active = false;
        }
        
        _mSplit.SpawnClone(0, _mSplit.mainClones, _startPos);
    }
}
