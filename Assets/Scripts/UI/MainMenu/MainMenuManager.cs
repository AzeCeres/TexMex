using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public SettingsController settingsController;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject gameLogo;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu();
        GetPlayerPrefs();
    }


    public void PlayGame()
    {
        //TODO: Play game code here.
        //Might add level select in polish.
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        gameLogo.SetActive(false);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        gameLogo.SetActive(true);
        //this is for testing the color saving to playeprefs
        GetPlayerPrefs();
    }

    private void GetPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("MasterVolume")) return;
        print("get player prefs");
        settingsController.masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        settingsController.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        settingsController.environmentVolume = PlayerPrefs.GetFloat("EnvironmentVolume");
        settingsController.playerVolume = PlayerPrefs.GetFloat("PlayerVolume");
        if (PlayerPrefs.HasKey("PlayerOneColor"))
        {
            ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("PlayerOneColor"), out settingsController.player1Color);
            settingsController.SetPlayerOneColor();
        }

        if (PlayerPrefs.HasKey("PlayerTwoColor"))
        {
            ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("PlayerTwoColor"), out settingsController.player2Color);
            settingsController.SetPlayerTwoColor();
        }

        if (PlayerPrefs.HasKey("PlayerThreeColor"))
        {
            ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("PlayerThreeColor"), out settingsController.player3Color);
            settingsController.SetPlayerThreeColor();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
