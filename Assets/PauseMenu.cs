using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuSetting;
    public GameObject PauseMenuSettingUI;
    public GameObject pauseMenuUI;
    public GameObject pauseMenu;

    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuSetting.SetActive(false);
            if (isPaused)
            {
                Resume(); 
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
<<<<<<< Updated upstream:Assets/PauseMenu.cs
=======

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
>>>>>>> Stashed changes:Assets/Scripts/PauseMenu/PauseMenu.cs
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
<<<<<<< Updated upstream:Assets/PauseMenu.cs
=======

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
>>>>>>> Stashed changes:Assets/Scripts/PauseMenu/PauseMenu.cs
    }

    public void Setting()
    {
        PauseMenuSetting.SetActive(true );
        PauseMenuSettingUI.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
<<<<<<< Updated upstream:Assets/PauseMenu.cs
}

=======

    public void QuitGame()
    {
        Application.Quit();
    }
}
>>>>>>> Stashed changes:Assets/Scripts/PauseMenu/PauseMenu.cs
