using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public partial class SettingsManager : MonoBehaviour
{
    [Header("Objects")]
    public AudioMixer audioMixer;
    
    [Header("Settings")]
    public float MasterVolume
    {
        get => _floatSettings[Settings.MasterVolume];
        set
        {
            _floatSettings[Settings.MasterVolume] = value; 
            SetMasterVolume(value);
        }
    }

    public float MusicVolume
    {
        get => _floatSettings[Settings.MusicVolume];
        set
        {
            _floatSettings[Settings.MusicVolume] = value;
            SetMusicVolume(value);
        }
    }

    public float SfxVolume
    {
        get => _floatSettings[Settings.SfxVolume];
        set
        {
            _floatSettings[Settings.SfxVolume] = value;
            SetSfxVolume(value);
        }
    }
    
    private Dictionary<string, float> _floatSettings = new()
    {
        { Settings.MasterVolume, 1f },
        { Settings.MusicVolume, 1f },
        { Settings.SfxVolume, 1f },
    };

    private void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", value);
    }

    private void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", value);
    }

    private void SetSfxVolume(float value)
    {
        audioMixer.SetFloat("SfxVolume", value);
    }
    
    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        foreach (var key in _floatSettings.Keys)
        {
            _floatSettings[key] = PlayerPrefs.GetFloat(key, _floatSettings[key]);
        }
    }
    
    public void SaveSettings()
    {
        foreach (var key in _floatSettings.Keys)
        {
            PlayerPrefs.SetFloat(key, _floatSettings[key]);
        }
        PlayerPrefs.Save();
    }

    private record Settings
    {
        public const string
            MasterVolume = "MasterVolume",
            MusicVolume = "MusicVolume",
            SfxVolume = "SFXVolume";
    }
}