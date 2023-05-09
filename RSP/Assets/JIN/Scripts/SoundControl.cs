using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;

    public void GameSceneBGMControl()
    {
        SoundManager.Instance.BGMVolume(BGMSlider.value);
    }

    public void GameSceneSFXControl()
    {
        SoundManager.Instance.SFXVolume(SFXSlider.value);
    }
}
