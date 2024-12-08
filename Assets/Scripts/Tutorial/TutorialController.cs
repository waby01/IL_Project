using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public Button continueButton;

    void Start()
    {
        continueButton.onClick.AddListener(ContinueToGame);
    }

    void ContinueToGame()
    {
        SceneManager.LoadScene("Laut");
    }
}
