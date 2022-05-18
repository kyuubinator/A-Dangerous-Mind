using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject exitGameConfirmation;
    private bool pauseActive;
    private bool optionActive;

    private void Start()
    {
        mainMenu?.SetActive(false);
        pauseMenu?.SetActive(false);
        optionsMenu?.SetActive(false);
        exitGameConfirmation?.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TryToPause();
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
        pauseMenu?.SetActive(true);
        //Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void UnPause()
    {
        pauseMenu?.SetActive(false);
        optionsMenu?.SetActive(false);
        exitGameConfirmation?.SetActive(false);
        //Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
}
