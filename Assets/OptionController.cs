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
    public Slider sfxVolSlider;
    [SerializeField] Toggle speedRunTimerToggle;

    float masterVolume;
    public float sfxVolume;

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

        _mixer.SetFloat("SFX", handler.sfxVolumeValue);

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
