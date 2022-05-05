using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private SettingsController settingsController;
    private int _index;

    private void OriginalRandomColor()
    {
        if (settingsController.randomColor)
        {
            if (this.gameObject == this.transform.parent.GetChild(0).gameObject)
            {
                settingsController.player1Color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                settingsController.SetPlayerOneColor();
            }
            else if (this.gameObject == this.transform.parent.GetChild(1).gameObject)
            {
                settingsController.player2Color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                settingsController.SetPlayerTwoColor();
            }
            else if (this.gameObject == this.transform.parent.GetChild(2).gameObject)
            {
                settingsController.player3Color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                settingsController.SetPlayerThreeColor();
            }
        }
    }

    private void OnEnable()
    {
        //for testing. add ! here and don't be an idiot like me
        if (settingsController.randomColor)
        {
            if (this.gameObject == this.transform.parent.GetChild(0).gameObject)
            {
                RollIndex();
                settingsController.player1Color = settingsController.colorList[_index];
                settingsController.SetPlayerOneColor();
            }
            else if (this.gameObject == this.transform.parent.GetChild(1).gameObject)
            {
                RollIndex();
                settingsController.player2Color = settingsController.colorList[_index];
                
                //TODO: Instead make copy of list and remove the matching color
                    while (settingsController.player2Color == settingsController.player1Color)
                    {
                        print("Rerolling");
                        RollIndex();
                        settingsController.player2Color = settingsController.colorList[_index];
                    }
                    settingsController.SetPlayerTwoColor();
            }
            else if (this.gameObject == this.transform.parent.GetChild(2).gameObject)
            {
                RollIndex();
                settingsController.player3Color = settingsController.colorList[_index];
                while (settingsController.player3Color == settingsController.player1Color || settingsController.player3Color == settingsController.player2Color)
                {
                    print("Rerolling");
                    RollIndex();
                    settingsController.player3Color = settingsController.colorList[_index];
                }
                settingsController.SetPlayerThreeColor();
            }
        }
    }

    private void RollIndex()
    {
        _index = Random.Range(0, settingsController.colorList.Count);
    }
}
