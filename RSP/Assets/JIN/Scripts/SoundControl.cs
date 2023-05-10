using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;

    private void Start()
    {
        BGMSlider.value = SoundManager.Instance.bSound;
        SFXSlider.value = SoundManager.Instance.sSound;
    }


    public void GameSceneBGMControl()
    {
        SoundManager.Instance.BGMVolume(BGMSlider.value);
    }

    public void GameSceneSFXControl()
    {
        SoundManager.Instance.SFXVolume(SFXSlider.value);
    }
}
