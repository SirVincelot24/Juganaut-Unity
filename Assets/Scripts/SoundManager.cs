using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("SFX")]
    public AudioSource collectDiamond;
    public AudioSource winGame;
    [Header("Music")]
    public AudioSource music;

    public void PlaySfx(SfxType type)
    {
        switch (type)
        {
            case SfxType.CollectDiamond:
                collectDiamond.Play();
                break;
            case SfxType.WinGame:
                winGame.Play();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
            
        }
    }

    public void StopMusic()
    {
        music.Stop();
    }
}

public enum SfxType
{
    CollectDiamond,
    WinGame
}