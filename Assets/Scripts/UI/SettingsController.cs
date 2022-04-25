using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "File Name", menuName = "ScriptableObjects/SettingsController")]
public class SettingsController : ScriptableObject
{
    #region Volume Values
    public float masterVolume;
    public float musicVolume;
    public float environmentVolume;
    public float playerVolume;
    

    #endregion

    #region Accessibility Values

    public Color player1Default;
    public Color player1Color;

    public Color player2Default;
    public Color player2Color;

    public Color player3Default;
    public Color player3Color;
    
    //add more here as needed

    #endregion
}

