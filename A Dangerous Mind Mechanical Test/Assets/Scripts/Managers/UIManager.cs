using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject exitGameConfirmation;
    [SerializeField] private Dropdown screenMode;
    [SerializeField] private Dropdown screenRes;
    private FullScreenMode currentMode;
    private bool pauseActive;
    private bool optionActive;

    public bool PauseActive { get => pauseActive; set => pauseActive = value; }

    private void Start()
    {
        DisableMenus();
        ResetRes();
    }

    private void Update()
    {
        if (!mainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TryToPause();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void TryToPause()
    {
        pauseActive = !pauseActive;
        if (pauseActive)
        {
            PauseMenu();
        }
        else
        {
            UnPause();
        }
    }

    private void PauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        //Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void UnPause()
    {
        DisableMenus();
        //Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        optionActive = false;
    }

    public void OptionsMenuMM()
    {
        optionActive = !optionActive;
        if (optionActive)
        {
            optionsMenu?.SetActive(true);
        }
        else
        {
            optionsMenu?.SetActive(false);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitConfShow()
    {
        exitGameConfirmation?.SetActive(true);
    }

    public void ExitConfHide()
    {
        exitGameConfirmation?.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void DisableMenus()
    {
        if (mainMenu != null)
            mainMenu.SetActive(false);
        if (pauseMenu != null)
            pauseMenu?.SetActive(false);
        if (optionsMenu != null)
            optionsMenu.SetActive(false);
        if (exitGameConfirmation != null)
            exitGameConfirmation.SetActive(false);
    }
    
    private void ResetRes()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.MaximizedWindow);
    }

    public void DropDownChangeScreenMode()
    {
        if (screenMode.value == 0)
        {
            currentMode = Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (screenMode.value == 1)
        {
            currentMode = Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (screenMode.value == 2)
        {
            currentMode = Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void DropDownChangeScreenRes()
    {
        if (screenRes.value == 0)
        {
            Screen.SetResolution(1920, 1080, currentMode);
        }
        else if (screenRes.value == 1)
        {
            Screen.SetResolution(1280, 720, currentMode);
        }
        else if (screenRes.value == 2)
        {
            Screen.SetResolution(960, 540, currentMode);
        }
    }
}
