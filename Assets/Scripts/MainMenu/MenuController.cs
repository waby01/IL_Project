using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private GameObject comfirmationPromt = null;
    [SerializeField] private float defaulVolume = 1.0f;

<<<<<<< Updated upstream
    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text ControllerSenTextValue = null;
    [SerializeField] private Slider ControllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSaveGameDialog = null;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndext = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndext = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndext;
        resolutionDropdown.RefreshShownValue();
=======

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        AudioClip clip = Resources.Load<AudioClip>("Audio/MainMenuMusic");
        if (clip != null && mainMenuMusicSource != null)
        {
            mainMenuMusicSource.clip = clip;
            mainMenuMusicSource.Play();
        }
        else
        {
            Debug.LogError("Audio clip tidak ditemukan atau AudioSource belum diatur!");
        }


        float savedVolume = PlayerPrefs.GetFloat("masterVolume", defaultVolume);
        SetVolume(savedVolume);
        volumeSlider.value = savedVolume;

        if (mainMenuMusicSource != null)
        {
            mainMenuMusicSource.volume = savedVolume;
        }
>>>>>>> Stashed changes
    }

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

<<<<<<< Updated upstream
    public void LoadGameDialogYes()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSaveGameDialog.SetActive(true);
        }
    }
=======
>>>>>>> Stashed changes

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
<<<<<<< Updated upstream
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
=======
        if (volume == 0)
        {
            volume = defaultVolume;
        }

        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");

        if (mainMenuMusicSource != null)
        {
            mainMenuMusicSource.volume = volume;
        }
>>>>>>> Stashed changes
    }

    public void VolumeApply()
    {
<<<<<<< Updated upstream
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void SetControllerSen(float sensitivity)
=======
        PlayerPrefs.SetFloat("BGMVolume", mainMenuMusicSource.volume);
        PlayerPrefs.SetFloat("SoundVolume", AudioListener.volume);
        PlayerPrefs.Save();
        StartCoroutine(ConfirmationBox());
    }


    public void ResetButton(string menuType)
>>>>>>> Stashed changes
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        ControllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
        }

        PlayerPrefs.SetFloat("masterSen", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }
    
    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen",(_isFullScreen ? 1:0));
        Screen.fullScreen = _isFullScreen;

        StartCoroutine(ConfirmationBox());
    }


    public void ResetButton(string MenuType)
    {
        if(MenuType == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }

        if(MenuType == "Audio")
        {
            AudioListener.volume = defaulVolume;
            volumeSlider.value = defaulVolume;
            volumeTextValue.text = defaulVolume.ToString("0.0");
            VolumeApply();
        }
<<<<<<< Updated upstream

        if(MenuType == "Gameplay")
        {
            ControllerSenTextValue.text = defaultSen.ToString("0");
            ControllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            invertYToggle.isOn = false;
            GameplayApply();
        }
=======
>>>>>>> Stashed changes
    }

    public IEnumerator ConfirmationBox()
    {
        comfirmationPromt.SetActive(true);
        yield return new WaitForSeconds(1);
        comfirmationPromt.SetActive(false);
    }
}