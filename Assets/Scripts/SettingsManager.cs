using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("Objects")] public AudioMixer audioMixer;

    [Header("Settings")]
    [CreateProperty]
    public float MasterVolume
    {
        get => _floatSettings[Settings.MasterVolume];
        set
        {
            _floatSettings[Settings.MasterVolume] = value;
            SetMasterVolume(value);
        }
    }
    [CreateProperty]
    public float MusicVolume
    {
        get => _floatSettings[Settings.MusicVolume];
        set
        {
            _floatSettings[Settings.MusicVolume] = value;
            SetMusicVolume(value);
        }
    }
    [CreateProperty]
    public float SfxVolume
    {
        get => _floatSettings[Settings.SfxVolume];
        set
        {
            _floatSettings[Settings.SfxVolume] = value;
            SetSfxVolume(value);
        }
    }
    [CreateProperty]
    public int Width
    {
        get => _intSettings[Settings.Width];
        set => _intSettings[Settings.Width] = value;
    }
    [CreateProperty]
    public int Height
    {
        get => _intSettings[Settings.Height];
        set => _intSettings[Settings.Height] = value;
    }
    [CreateProperty]
    public Vector2 Diamonds
    {
        get => new(_intSettings[Settings.DiamondsMin], _intSettings[Settings.DiamondsMax]);
        set
        {
            _intSettings[Settings.DiamondsMin] = (int)value.x;
            _intSettings[Settings.DiamondsMax] = (int)value.y;
        }
    }
    [CreateProperty]
    public Vector2 Monster
    {
        get => new(_intSettings[Settings.MonsterMin], _intSettings[Settings.MonsterMax]);
        set
        {
            _intSettings[Settings.MonsterMin] = (int)value.x;
            _intSettings[Settings.MonsterMax] = (int)value.y;
        }
    }
    [CreateProperty]
    public Vector2 Bombs
    {
        get => new(_intSettings[Settings.BombsMin], _intSettings[Settings.BombsMax]);
        set
        {
            _intSettings[Settings.BombsMin] = (int)value.x;
            _intSettings[Settings.BombsMax] = (int)value.y;
        }
    }
    [CreateProperty]
    public Vector2 Rocks
    {
        get => new(_intSettings[Settings.RocksMin], _intSettings[Settings.RocksMax]);
        set
        {
            _intSettings[Settings.RocksMin] = (int)value.x;
            _intSettings[Settings.RocksMax] = (int)value.y;
        }
    }
    
    private readonly Dictionary<string, float> _floatSettings = new(StandardSettings.FloatSettings);

    private readonly Dictionary<string, int> _intSettings = new(StandardSettings.IntSettings);

    public void ResetWorldSettings()
    {
        Width = StandardSettings.IntSettings[Settings.Width];
        Height = StandardSettings.IntSettings[Settings.Height];
        Diamonds = new Vector2(StandardSettings.IntSettings[Settings.DiamondsMin],
            StandardSettings.IntSettings[Settings.DiamondsMax]);
        Monster = new Vector2(StandardSettings.IntSettings[Settings.MonsterMin],
            StandardSettings.IntSettings[Settings.MonsterMax]);
        Bombs = new Vector2(StandardSettings.IntSettings[Settings.BombsMin],
            StandardSettings.IntSettings[Settings.BombsMax]);
        Rocks = new Vector2(StandardSettings.IntSettings[Settings.RocksMin],
            StandardSettings.IntSettings[Settings.RocksMax]);
    }

    private void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    private void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    private void SetSfxVolume(float value)
    {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(value) * 20);
    }

    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        foreach (var key in new List<string>(_floatSettings.Keys))
        {
            ApplySetting(key, GetFloatSetting(key));
        }

        foreach (var key in new List<string>(_intSettings.Keys))
        {
            ApplySetting(key, GetIntSetting(key));
        }
    }

    public void SaveSettings()
    {
        foreach (var key in _floatSettings.Keys)
        {
            PlayerPrefs.SetFloat(key, _floatSettings[key]);
        }

        foreach (var key in _intSettings.Keys)
        {
            PlayerPrefs.SetInt(key, _intSettings[key]);
        }

        PlayerPrefs.Save();
    }

    private int GetIntSetting(string key)
    {
        return !_intSettings.TryGetValue(key, out var setting)
            ? throw new KeyNotFoundException(key)
            : PlayerPrefs.GetInt(key, setting);
    }

    private float GetFloatSetting(string key)
    {
        return !_floatSettings.TryGetValue(key, out var setting)
            ? throw new KeyNotFoundException(key)
            : PlayerPrefs.GetFloat(key, setting);
    }

    private void ApplySetting<T>(string key, T value)
    {
        switch (value)
        {
            case float floatValue:
                switch (key)
                {
                    case Settings.MasterVolume:
                        MasterVolume = floatValue;
                        break;
                    case Settings.MusicVolume:
                        MusicVolume = floatValue;
                        break;
                    case Settings.SfxVolume:
                        SfxVolume = floatValue;
                        break;
                }

                break;
            case int intValue:
                switch (key)
                {
                    case Settings.Width:
                        Width = intValue;
                        break;
                    case Settings.Height:
                        Height = intValue;
                        break;
                    case Settings.DiamondsMin:
                    case Settings.DiamondsMax:
                        Diamonds = new Vector2(
                            GetIntSetting(Settings.DiamondsMin),
                            GetIntSetting(Settings.DiamondsMax));
                        break;
                    case Settings.MonsterMin:
                    case Settings.MonsterMax:
                        Monster = new Vector2(
                            GetIntSetting(Settings.MonsterMin),
                            GetIntSetting(Settings.MonsterMax));
                        break;
                    case Settings.BombsMin:
                    case Settings.BombsMax:
                        Bombs = new Vector2(
                            GetIntSetting(Settings.BombsMin),
                            GetIntSetting(Settings.BombsMax));
                        break;
                    case Settings.RocksMin:
                    case Settings.RocksMax:
                        Rocks = new Vector2(
                            GetIntSetting(Settings.RocksMin),
                            GetIntSetting(Settings.RocksMax));
                        break;
                }

                break;
        }
    }
}

internal static class Settings
{
    public const string
        MasterVolume = "MasterVolume",
        MusicVolume = "MusicVolume",
        SfxVolume = "SFXVolume",
        
        Width = "Width",
        Height = "Height",
        DiamondsMin = "DiamondsMin",
        DiamondsMax = "DiamondsMax",
        MonsterMin = "MonsterMin",
        MonsterMax = "MonsterMax",
        BombsMin = "BombsMin",
        BombsMax = "BombsMax",
        RocksMin = "RocksMin",
        RocksMax = "RocksMax"
        ;
}
internal static class StandardSettings
{
    public static readonly Dictionary<string, float> FloatSettings = new()
    {
        { Settings.MasterVolume, 1f },
        { Settings.MusicVolume, 1f },
        { Settings.SfxVolume, 1f },
    };

    public static readonly Dictionary<string, int> IntSettings = new()
    {
        { Settings.Width, 20 },
        { Settings.Height, 20 },
        { Settings.DiamondsMin, 10 },
        { Settings.DiamondsMax, 30 },
        { Settings.MonsterMin, 20 },
        { Settings.MonsterMax, 50 },
        { Settings.BombsMin, 10 },
        { Settings.BombsMax, 20 },
        { Settings.RocksMin, 20 },
        { Settings.RocksMax, 50 },
    };
}