using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("SFX")]
    public List<AudioSource> sfxList;
    [Header("Music")]
    public AudioSource music;

    public void PlaySfx(SfxType type)
    {
        sfxList[(int)type].Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }
}

public enum SfxType
{
    CollectDiamond = 0,
    Win = 1,
    GameOver = 2
}