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
}

