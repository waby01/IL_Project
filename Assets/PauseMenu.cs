using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Drag PauseMenuUI panel here in the Inspector

    private bool isPaused = false;

    void Update()
    {
        // Cek jika tombol "Esc" ditekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume(); // Melanjutkan permainan jika sudah dalam keadaan pause
            }
            else
            {
                Pause(); // Menghentikan permainan jika belum dalam keadaan pause
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Sembunyikan pause menu
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Tampilkan pause menu
        Time.timeScale = 0f; // Pause game time
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Pastikan game time di-reset ke normal
        SceneManager.LoadScene("MainMenu"); // Ganti "MainMenu" dengan nama scene main menu Anda
    }
}

