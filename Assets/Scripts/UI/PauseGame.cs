using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private InputAction pauseButton;
    [SerializeField] private GameObject[] pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject resumeButton;

    private bool paused = false;

    private void OnEnable()
    {
        pauseButton.Enable();
        Cursor.visible = false; 
    }

    private void OnDisable()
    {
        pauseButton.Disable();
    }

    private void Update()
    {
        print(paused);
    }

    private void Start()
    {
        foreach (var i in pauseMenu)
        {
            i.SetActive(false);
        }
        pauseButton.performed += _ => Pause();
    }

    public void Pause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            foreach (var i in pauseMenu)
            {
                i.SetActive(false);
            }
            pauseMenu[0].SetActive(true);
            settingsMenu.SetActive(false);
            eventSystem.SetSelectedGameObject(resumeButton);
        }
        else
        {
            Time.timeScale = 1;
            foreach (var i in pauseMenu)
            {
                i.SetActive(false);
            }
            settingsMenu.GetComponent<OptionsMenu>().AccessibilityMenu();
            print("Unpausing game");
            settingsMenu.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    public void BackToPauseMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu[0].SetActive(true);
    }

    public void OptionsButton()
    {
        foreach (var i in pauseMenu)
        {
            i.SetActive(false);
        }
        settingsMenu.SetActive(true);
    }
    public void MainMenu()
    {
        foreach (var i in pauseMenu)
        {
            i.SetActive(false);
        }
        pauseMenu[1].SetActive(true);
    }
    public void CancelQuit()
    {
        foreach (var i in pauseMenu)
        {
            i.SetActive(false);
        }
        pauseMenu[0].SetActive(true);
    }
}
