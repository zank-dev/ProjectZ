using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropDown;
    public TMP_Dropdown qualityDropDown;
    public Toggle fullScreenToggle;
    public Slider soundSlider;

    private Resolution[] resolutions;

    private void Start()
    {
        FillResolutionDropdownMenu();

        qualityDropDown.value = QualitySettings.GetQualityLevel(); // gets the quality setting
        qualityDropDown.RefreshShownValue();

        fullScreenToggle.isOn = Screen.fullScreen; // sets the toggle

        // sets the audio volume, if there is a saved value otherwise uses default 
        audioMixer.SetFloat("mainVolume", PlayerPrefs.GetFloat("mainVolume", 0));
        soundSlider.value = PlayerPrefs.GetFloat("mainVolume", 0); // displays the audio value
    }

    private void FillResolutionDropdownMenu()
    {
        resolutions = Screen.resolutions; // gets all possible resolutions
        resolutionDropDown.ClearOptions(); // removes all options in the dropdown menu

        List<String> resolutionList = new List<String>(); // creats a list of resolutions

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) // goes trough all resolutions and adds them to the list with extra info
        {
            String resolution = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "Hz";
            resolutionList.Add(resolution);

            // when the current resolution is the same as the resolution from the possible resolutions. saves the index
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i - resolutions.Length + 1; // reverses value
        }

        resolutionList.Reverse(); // reverses list (nobody is using 800 x 600, etc.)
        resolutionDropDown.AddOptions(resolutionList);
        if (PlayerPrefs.HasKey("resolutionIndex"))
            resolutionDropDown.value = resolutions.Length - 1 - PlayerPrefs.GetInt("resolutionIndex"); // loads the resolution 
        else resolutionDropDown.value = currentResolutionIndex; // else uses the index 

        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolutionSetting(int index)
    {
        Resolution resolution = resolutions[resolutions.Length - 1 - index]; // gets the resolution from reversed index
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); // sets the resolution 
        PlayerPrefs.SetInt("resolutionIndex", (resolutions.Length - 1 - index)); // stores the reversed index
    }

    public void SetVolumeSettings(float volume)
    {
        audioMixer.SetFloat("mainVolume", volume); // sets volume from hud slider
        PlayerPrefs.SetFloat("mainVolume", volume); // saves the value
    }

    public void SetQualitySettings(int index)
    {
        QualitySettings.SetQualityLevel(index); 
    }

    public void SetFullScreenSetting(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    // when the player ends the game (doesn't matter how), stores the playtime + already stored playtime
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("playTime", Time.realtimeSinceStartup + PlayerPrefs.GetFloat("playTime"));
    }
}