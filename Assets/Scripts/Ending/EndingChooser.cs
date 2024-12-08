using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingChooser : MonoBehaviour
{
    public void GoToEnding1()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Ending1");
    }

    public void GoToEnding2()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Ending2");
    }
}
