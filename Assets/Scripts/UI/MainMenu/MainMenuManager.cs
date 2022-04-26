using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public SettingsController settingsController;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
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
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    private void GetPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("MasterVolume")) return;
        print("get player prefs");
        settingsController.masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        settingsController.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        settingsController.environmentVolume = PlayerPrefs.GetFloat("EnvironmentVolume");
        settingsController.playerVolume = PlayerPrefs.GetFloat("PlayerVolume");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
