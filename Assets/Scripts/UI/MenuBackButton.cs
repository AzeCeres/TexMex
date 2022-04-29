using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuBackButton : MonoBehaviour
{
    [SerializeField] private InputAction backButton;

    [SerializeField] private GameObject[] backButtonObjects;

    private void OnEnable()
    {
        backButton.Enable();
    }

    private void OnDisable()
    {
        backButton.Disable();
    }

    private void Start()
    {
        backButton.performed += _ => BackButtonScript();
    }

    public void BackButtonScript()
    {
        foreach (var i in backButtonObjects)
        {
            if (i.activeInHierarchy == true)
            {
                i.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
