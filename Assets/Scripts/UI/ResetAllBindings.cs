using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ResetAllBindings : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private GameObject[] keyboardButtons;
    [SerializeField] private GameObject[] controllerButtons;

    public void ResetKeyboardBindings()
    {
        foreach (InputActionMap map in inputActions.actionMaps)
        {
            //map.RemoveBindingOverrides();
            foreach (var i in keyboardButtons)
            {
                i.GetComponent<Button>().onClick.Invoke();   
            }
        }
        //PlayerPrefs.DeleteKey("rebinds");
    }

    public void ResetControllerBindings()
    {
        foreach (var i in controllerButtons)
        {
            i.GetComponent<Button>().onClick.Invoke(); 
        }
    }
}
