using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public SettingsController settingsController;

    [SerializeField] private GameObject[] optionsMenus;
    [SerializeField] private Slider[] volumeSliders;
    


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
        SaveVolume();
        SetSliderValue();
    }

    public void SetVolume()
    {
        settingsController.masterVolume = volumeSliders[0].value;
        settingsController.musicVolume = volumeSliders[1].value;
        settingsController.environmentVolume = volumeSliders[2].value;
        settingsController.playerVolume = volumeSliders[3].value;
    }

    private void SetSliderValue()
    {
        volumeSliders[0].value = PlayerPrefs.GetFloat("MasterVolume");
        volumeSliders[1].value = PlayerPrefs.GetFloat("MusicVolume");
        volumeSliders[2].value = PlayerPrefs.GetFloat("EnvironmentVolume");
        volumeSliders[3].value = PlayerPrefs.GetFloat("PlayerVolume");
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("MasterVolume", settingsController.masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", settingsController.musicVolume);
        PlayerPrefs.SetFloat("EnvironmentVolume", settingsController.environmentVolume);
        PlayerPrefs.SetFloat("PlayerVolume", settingsController.playerVolume);
    }
    
    public void VolumeBackButton()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            settingsController.masterVolume = 100;
            settingsController.musicVolume = 100;
            settingsController.environmentVolume = 100;
            settingsController.playerVolume = 100;
            return;
        }
        settingsController.masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        settingsController.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        settingsController.environmentVolume = PlayerPrefs.GetFloat("EnvironmentVolume");
        settingsController.playerVolume = PlayerPrefs.GetFloat("PlayerVolume");
    }
}
