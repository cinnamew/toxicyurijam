using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySettings : MonoBehaviour
{
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown fpsDropdown;
    private List<Resolution> supportedResolutions;
    private readonly int[] fpsSelections = { 30, 60, 90, -1 };


    private void Awake()
    {
        supportedResolutions = new List<Resolution>();
        resolutionDropdown.ClearOptions();
        foreach (Resolution res in Screen.resolutions)
        {
            if (Mathf.Approximately((float)res.refreshRateRatio.value, Mathf.Floor((float)res.refreshRateRatio.value)))
            {
                supportedResolutions.Add(res);
                resolutionDropdown.options.Add(new(res.width + " x " + res.height + " @" + Math.Round(res.refreshRateRatio.value) + "Hz"));
            }
        }
        
        fpsDropdown.ClearOptions();
        foreach (int fps in fpsSelections)
        {
            if (fps == -1) fpsDropdown.options.Add(new("Unlimited FPS"));
            else fpsDropdown.options.Add(new(fps.ToString() + " FPS"));
        }
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        if (Screen.fullScreen) fullscreenToggle.isOn = true;

        if (PlayerPrefs.HasKey(Globals.FULLSCREEN)) LoadResolutionSave();
        else CreateResolutionSave();

        if (PlayerPrefs.HasKey(Globals.FPS)) LoadFpsSave();
        else CreateFpsSave();
    }

    public void ChangeResolution()
    {
        Screen.SetResolution(supportedResolutions[resolutionDropdown.value].width, supportedResolutions[resolutionDropdown.value].height, Screen.fullScreenMode, supportedResolutions[resolutionDropdown.value].refreshRateRatio);
        PlayerPrefs.SetInt(Globals.RES_WIDTH, supportedResolutions[resolutionDropdown.value].width);
        PlayerPrefs.SetInt(Globals.RES_HEIGHT, supportedResolutions[resolutionDropdown.value].height);
        PlayerPrefs.SetFloat(Globals.REFRESH_RATE, (float)Math.Round(supportedResolutions[resolutionDropdown.value].refreshRateRatio.value, 2));
    }

    public void ChangeFps()
    {
        Application.targetFrameRate = fpsSelections[fpsDropdown.value];
        PlayerPrefs.SetInt(Globals.FPS, fpsDropdown.value);
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        PlayerPrefs.SetInt(Globals.FULLSCREEN, fullscreenToggle.isOn ? 1 : 0);
    }

    private void CreateFpsSave()
    {
        fpsDropdown.value = fpsSelections.Length - 1;
    }

    private void LoadFpsSave()
    {
        fpsDropdown.value = PlayerPrefs.GetInt(Globals.FPS);
    }

    private void CreateResolutionSave()
    {
        for (int i = 0; i < supportedResolutions.Count; i++)
        {
            if (supportedResolutions[i].Equals(Screen.currentResolution))
            {
                resolutionDropdown.value = i;
                PlayerPrefs.SetInt(Globals.RES_WIDTH, Screen.currentResolution.width);
                PlayerPrefs.SetInt(Globals.RES_HEIGHT, Screen.currentResolution.height);
                PlayerPrefs.SetFloat(Globals.REFRESH_RATE, (float)Math.Round(Screen.currentResolution.refreshRateRatio.value, 2));
                break;
            }
        }
    }

    private void LoadResolutionSave()
    {
        for (int i = 0; i < supportedResolutions.Count; i++)
        {
            if (supportedResolutions[i].width == PlayerPrefs.GetInt(Globals.RES_WIDTH) && 
                supportedResolutions[i].height == PlayerPrefs.GetInt(Globals.RES_HEIGHT) &&
                (float)Math.Round(supportedResolutions[i].refreshRateRatio.value, 2) == PlayerPrefs.GetFloat(Globals.REFRESH_RATE))
            {
                resolutionDropdown.value = i;
                break;
            }
        }
        resolutionDropdown.RefreshShownValue();
        fullscreenToggle.isOn = PlayerPrefs.GetInt(Globals.FULLSCREEN) == 1;
        ChangeResolution();
    }
}
