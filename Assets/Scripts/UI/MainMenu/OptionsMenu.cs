using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public SettingsController settingsController;

    [SerializeField] private GameObject[] optionsMenus;
    [SerializeField] private Slider[] volumeSliders;
    
    [SerializeField] private GameObject[] controlsMenus;
    
    [SerializeField] private GameObject[] accessibilityMenus;
    
    [SerializeField] private GameObject[] playerPreview;
    [SerializeField] private GameObject gamepadCursor;
    [SerializeField] private GameObject gamepadManager;
    [SerializeField] private Toggle randomToggle;



//Ensures that the main options menu always appears first no matter what we enable or disable in the editor.
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
        Cursor.visible = true;
        gamepadCursor.SetActive(false);
    }

    //Opens volume menu. Called from UI button
    public void VolumeMenu()
    {
        foreach (var i in optionsMenus)
        {
            i.SetActive(false);
        }
        optionsMenus[1].SetActive(true);
        //Saves volume from SettingsController to playerprefs, then sets the slider values accordingly
        //Doing this instead of getting info right from the scrub to avoid unexplainable bug
        SaveVolume();
        SetSliderValue();
    }
    
    public void ControlsMenu()
    {
        foreach (var i in optionsMenus)
        {
            i.SetActive(false);
        }
        optionsMenus[2].SetActive(true);
        foreach (var p in controlsMenus)
        {
            p.SetActive(false);
        }
        controlsMenus[0].SetActive(true);
    }
    
    public void AccessibilityMenu()
    {
        foreach (var i in optionsMenus)
        {
            i.SetActive(false);
        }
        optionsMenus[3].SetActive(true);
        foreach (var p in accessibilityMenus)
        {
            p.SetActive(false);
        }
        accessibilityMenus[0].SetActive(true);
        foreach (var i in playerPreview)
        {
            i.SetActive(false);
        }
        gamepadCursor.SetActive(false);
        gamepadManager.SetActive(false);
        settingsController.randomColor = false;
    }

    #region Accessibility Options
    
    
    //The Color PickerMenus
    public void ColorPickerMenu(int selectedMenu)
    {
        foreach (var i in accessibilityMenus)
        {
            i.SetActive(false);
        }
        accessibilityMenus[selectedMenu].SetActive(true);
        gamepadCursor.SetActive(true);
        gamepadManager.SetActive(true);

        if (selectedMenu == 1)
        {
            playerPreview[0].SetActive(true);
        }
        else if (selectedMenu == 2)
        {
            playerPreview[1].SetActive(true);
        }
        else if (selectedMenu == 3)
        {
            playerPreview[2].SetActive(true);
        }
    }

    public void ToggleRandomColor()
    {
        settingsController.randomColor = !settingsController.randomColor;
    }
    #endregion

    #region Controls Options
    public void KeyboardControlsMenu()
    {
        foreach (var i in controlsMenus)
        {
            i.SetActive(false);
        }
        controlsMenus[1].SetActive(true);
    }
    public void GamepadControlsMenu()
    {
        foreach (var i in controlsMenus)
        {
            i.SetActive(false);
        }
        controlsMenus[2].SetActive(true);
    }


    #endregion

    #region Volume Options
    //Called from unity slider value change
    public void SetVolume()
    {
        settingsController.masterVolume = volumeSliders[0].value;
        settingsController.musicVolume = volumeSliders[1].value;
        settingsController.environmentVolume = volumeSliders[2].value;
        settingsController.playerVolume = volumeSliders[3].value;
    }

    //Sets the slider values
    private void SetSliderValue()
    {
        volumeSliders[0].value = PlayerPrefs.GetFloat("MasterVolume");
        volumeSliders[1].value = PlayerPrefs.GetFloat("MusicVolume");
        volumeSliders[2].value = PlayerPrefs.GetFloat("EnvironmentVolume");
        volumeSliders[3].value = PlayerPrefs.GetFloat("PlayerVolume");
    }

    //Saves volume to player prefs. Called from UI button
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("MasterVolume", settingsController.masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", settingsController.musicVolume);
        PlayerPrefs.SetFloat("EnvironmentVolume", settingsController.environmentVolume);
        PlayerPrefs.SetFloat("PlayerVolume", settingsController.playerVolume);
    }
    
    //Sets volume back to what it was before volume change. Called from UI button
    public void VolumeBackButton()
    {
        //Sets volume to 100 if there are no playerprefs saved.
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            settingsController.masterVolume = 100;
            settingsController.musicVolume = 100;
            settingsController.environmentVolume = 100;
            settingsController.playerVolume = 100;
            return;
        }
        //Scrub loads volume from player prefs
        settingsController.masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        settingsController.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        settingsController.environmentVolume = PlayerPrefs.GetFloat("EnvironmentVolume");
        settingsController.playerVolume = PlayerPrefs.GetFloat("PlayerVolume");
    }
    #endregion
}
