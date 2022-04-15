using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField] private GameObject[] optionsMenus;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        MainOptionsMenu();
    }

    // Update is called once per frame

    public void MainOptionsMenu()
    {
        foreach (var i in optionsMenus)
        {
            i.SetActive(false);
        }
        optionsMenus[0].SetActive(true);
    }

    public void VolumeMenu()
    {
        foreach (var i in optionsMenus)
        {
            i.SetActive(false);
        }
        optionsMenus[1].SetActive(true);
    }
}
