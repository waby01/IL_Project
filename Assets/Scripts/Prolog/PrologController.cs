using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class PrologController : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public string tutorialSceneName = "Tutorial";

    void Start()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }

        playableDirector.stopped += OnTimelineStopped;
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        LoadTutorialScene();
    }

    void LoadTutorialScene()
    {
        SceneManager.LoadScene(tutorialSceneName);
    }
}
