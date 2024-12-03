using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionController : MonoBehaviour
{
    OptionValueHandler handler;

    public static OptionController instance;

    [SerializeField] GameObject handlerPrefab;

    [SerializeField] Slider masterVolSlider;
    [SerializeField] Slider sfxVolSlider;
    [SerializeField] Slider uiVolSlider;
    [SerializeField] Slider backgroundVolSlider;
    [SerializeField] Toggle speedRunTimerToggle;

    float masterVolume;
    float sfxVolume;
    float uiVolume;
    float backgroundVolume;


    [SerializeField] private AudioMixer _mixer;

    private void Start()
    {
        instance = this;

        handler = FindAnyObjectByType<OptionValueHandler>();

        if (handler == null)
        {
            handler = Instantiate(handlerPrefab).GetComponent<OptionValueHandler>();
        }

        masterVolSlider.value = AudioListener.volume;
        sfxVolSlider.value = handler.sfxVolumeValue;
        uiVolSlider.value = handler.uiVolumeValue;
        backgroundVolSlider.value = handler.backgroundVolumeValue;

        _mixer.SetFloat("SFX", handler.sfxVolumeValue);
        _mixer.SetFloat("UI", handler.uiVolumeValue);
        _mixer.SetFloat("Background", handler.backgroundVolumeValue);

        if (handler.timerBool)
        {
            if(PauseMenuReference.instance != null)
            {
                PauseMenuReference.instance.speedRunTimer.SetActive(true);
            }

            speedRunTimerToggle.isOn = handler.timerBool;
        }
    }

    private void Update()
    {
        UpdateVolume();
        UpdateTimerToggle();
    }

    public void OnSFXVolumeChange (float value)
    {
        _mixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        
        if(handler != null)
        {
            handler.sfxVolumeValue = value;
        }
    }

    public void OnUIVolumeChange(float value)
    {
        _mixer.SetFloat("UI", Mathf.Log10(value) * 20);

        if(handler != null)
        {
            handler.uiVolumeValue = value;
        }
    }

    public void OnBackgroundVolumeChange(float value)
    {
        _mixer.SetFloat("Background", Mathf.Log10(value) * 20);

        if (handler != null)
        {
            handler.backgroundVolumeValue = value;
        }
    }

    private void UpdateVolume()
    {
        masterVolume = masterVolSlider.value;
        AudioListener.volume = masterVolume;
        
        if(handler != null)
        {
            handler.masterVolumeValue = AudioListener.volume;
        }
    }

    private void UpdateTimerToggle()
    {
        handler.timerBool = speedRunTimerToggle.isOn;
    }
}
