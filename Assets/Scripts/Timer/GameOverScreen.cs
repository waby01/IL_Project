using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public void setup()
    {
        gameObject.SetActive(true);
        

<<<<<<< Updated upstream
=======
        gameObject.SetActive(true);

        if (gameOverText != null)
        {
            gameOverText.text = message;
            Debug.Log("Game Over Text Set: " + message);
        }
        else
        {
            Debug.LogError("Game Over Text not assigned!");
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
>>>>>>> Stashed changes
    }

    public void restartButton()
    {
<<<<<<< Updated upstream
        
        SceneManager.LoadScene(0); // Reload the current scene
        
=======
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
>>>>>>> Stashed changes
    }


}
