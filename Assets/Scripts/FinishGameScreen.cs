using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameScreen : MonoBehaviour
{
    public GameObject finishScreen;
    public float delayToNextScene = 3f;
    public string nextSceneName = "StoryCutsceneEndGame";

    void Start()
    {

    }

    public void Setup()
    {
        finishScreen.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Invoke("GoToNextScene", delayToNextScene);
    }

    public void GoToNextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(5);
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
