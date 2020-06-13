using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    [SerializeField] private float audioLevel;

    private void Start()
    {
        audioMixer.GetFloat("volume", out audioLevel);
        slider.value = audioLevel;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void ToggleAudio (bool mute)
    {
        if (mute == false)
        {
            audioMixer.SetFloat("volume", audioLevel);
            slider.enabled = true;
        }
        if (mute == true)
        {
            audioMixer.GetFloat("volume", out audioLevel);
            audioMixer.SetFloat("volume", -80);
            slider.enabled = false;
        }
    }
}
