using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource collectDiamond;

    public void PlaySfx(SfxType type)
    {
        switch (type)
        {
            case SfxType.CollectDiamond:
                collectDiamond.Play();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}

public enum SfxType
{
    CollectDiamond,
}