using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsLoader : MonoBehaviour
{
    private void Start()
    {
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        AudioSource bgmSource = FindObjectOfType<AudioSource>();
        if (bgmSource != null)
            bgmSource.volume = bgmVolume;

        float sfxVolume = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
        AudioSource[] sfxSources = FindObjectsOfType<AudioSource>();
        foreach (var sfxSource in sfxSources)
        {
            if (sfxSource != bgmSource)
                sfxSource.volume = sfxVolume;
        }
    }
}