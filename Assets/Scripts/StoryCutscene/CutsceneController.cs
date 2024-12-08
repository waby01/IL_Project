using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CutsceneButtonController : MonoBehaviour
{
    public GameObject Button;
    public PlayableDirector playableDirector;
    private bool isCutsceneFinished = false;

    void Start()
    {
        Button.SetActive(false);

        playableDirector.stopped += OnCutsceneFinished;
    }

    void Update()
    {
        if (playableDirector.state == PlayState.Paused && !isCutsceneFinished)
        {
            isCutsceneFinished = true;
        }
    }

    private void OnCutsceneFinished(PlayableDirector director)
    {
        Debug.Log("Cutscene selesai, menunggu signal untuk berpindah scene.");
    }

    public void ShowButtons()
    {
        Debug.Log("ShowButton dipanggil oleh Signal");
        if (Button == null)
        {
            return;
        }
        Button.SetActive(true);
    }
    public void LoadChooseYourEnding()
    {
        Debug.Log("Menerima signal untuk berpindah ke ChooseYourEnding");
        SceneManager.LoadScene(6);
    }

    public void ChooseEnding1()
    {
        SceneManager.LoadScene(7);
    }

    public void ChooseEnding2()
    {
        SceneManager.LoadScene(8);
    }
}
