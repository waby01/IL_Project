using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Manager : MonoBehaviour
{
    public GameObject buttonMainMenu;
    

   public  void ShowMainMenuButton()
    {
        buttonMainMenu.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
