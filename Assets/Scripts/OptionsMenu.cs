using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resDropdown;

    Resolution[] resolutions;

    private void Start() {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for(int i=0;i<resolutions.Length;i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicParam",  volume);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXParam", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
