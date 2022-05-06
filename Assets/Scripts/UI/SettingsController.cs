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
    
    //This is for random color swapping. Just a setting you can enable for fun
    public bool randomColor;
    public List<Color> colorList = new List<Color>();
    
    
    
    //add more here as needed

    public Material playerOneMaterial;
    public Material playerTwoMaterial;
    public Material playerThreeMaterial;
    #endregion

    public void SetPlayerOneColor()
    {
        playerOneMaterial.SetColor("_Color", player1Color);
    }
    public void SetPlayerTwoColor()
    {
        playerTwoMaterial.SetColor("_Color", player2Color);
    }
    public void SetPlayerThreeColor()
    {
        playerThreeMaterial.SetColor("_Color", player3Color);
    }
}

