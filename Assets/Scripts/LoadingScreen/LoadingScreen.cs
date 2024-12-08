using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using System.Collections.Generic;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider progressBar;
    public TMP_Text progressText;
    public Image backgroundImage;
    public TMP_Text tipText;

    [Header("Backgrounds and Tips")]
    public Sprite[] backgrounds;
    public string[] tips;

    private Queue<string> sceneQueue;
    private PlayableDirector prologPlayableDirector;

    public Slider loadingSlider;
    public float targetProgress = 1f;
    public float duration = 5f;

        private void Start()
    {
        int backgroundIndex = PlayerPrefs.GetInt("BackgroundIndex", 0);
        if (backgrounds.Length > 0 && backgroundIndex < backgrounds.Length)
        {
            backgroundImage.sprite = backgrounds[backgroundIndex];
        }

        if (tips.Length > 0)
        {
            int randomIndex = Random.Range(0, tips.Length);
            tipText.text = tips[randomIndex];
        }

        if (progressBar != null) progressBar.value = 0f;
        if (progressText != null) progressText.text = "0%";

        LoadSceneOrder();
        StartCoroutine(ProcessSceneQueue());
    }

    public static void LoadScene(string sceneOrder, int backgroundIndex)
    {
        PlayerPrefs.SetString("SceneOrder", sceneOrder);
        PlayerPrefs.SetInt("BackgroundIndex", backgroundIndex);

        SceneManager.LoadScene("Loading_Screen");
    }

    private void LoadSceneOrder()
    {
        sceneQueue = new Queue<string>();

        sceneQueue.Enqueue("Prolog");
        sceneQueue.Enqueue("Tutorial");
        sceneQueue.Enqueue("Laut"); 
    }

    private IEnumerator ProcessSceneQueue()
    {
        while (sceneQueue.Count > 0)
        {
            string currentScene = sceneQueue.Dequeue();
            Debug.Log("Currently loading: " + currentScene);

            if (currentScene == "Prolog")
            {
                yield return LoadPrologSceneAsync();
            }
            else if (currentScene == "Tutorial")
            {
                yield return LoadTutorialSceneAsync();
            }
            else
            {
                yield return LoadSceneAsync(currentScene);
            }
        }
    }

    private IEnumerator LoadPrologSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Prolog");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (progressBar != null) progressBar.value = operation.progress;
            if (progressText != null) progressText.text = $"{(operation.progress * 100):0}%";

            if (operation.progress >= 0.9f)
            {
                prologPlayableDirector = FindObjectOfType<PlayableDirector>();
                if (prologPlayableDirector != null)
                {
                    yield return new WaitUntil(() => prologPlayableDirector.state != PlayState.Playing);
                }

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }



    private IEnumerator LoadTutorialSceneAsync()
    {
        Debug.Log("Memulai loading scene Tutorial...");
        AsyncOperation operation = SceneManager.LoadSceneAsync("Tutorial");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (progressBar != null) progressBar.value = operation.progress;
            if (progressText != null) progressText.text = $"{(operation.progress * 100):0}%";

            if (operation.progress >= 0.9f)
            {
                Debug.Log("Scene Tutorial siap diaktifkan");
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }


    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is empty or not set!");
            yield break;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float targetProgress = 1.0f;
        float currentProgress = 0.0f;
        float speed = 0.5f; 

        while (currentProgress < targetProgress)
        {
            currentProgress += speed; 
            currentProgress = Mathf.Clamp01(currentProgress);

            if (progressBar != null)
            {
                progressBar.value = currentProgress; 
            }

            if (progressText != null)
            {
                progressText.text = $"{(currentProgress * 100):0}%"; 
            }

            Debug.Log($"Progress: {currentProgress * 100}%"); 

            yield return null;
        }


        if (progressText != null)
        {
            progressText.text = "Loading Complete!";
        }
        yield return new WaitForSeconds(3f);

        operation.allowSceneActivation = true;
    }
}