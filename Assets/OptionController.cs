using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    OptionValueHandler handler;

    [SerializeField] GameObject handlerPrefab;

    [SerializeField] Slider masterVolSlider;
    [SerializeField] Toggle speedRunTimerToggle;

    float masterVolume;

    private void Start()
    {
        masterVolSlider.value = AudioListener.volume;

        handler = FindAnyObjectByType<OptionValueHandler>();

        if(handler == null)
        {
            handler = Instantiate(handlerPrefab).GetComponent<OptionValueHandler>();
        }

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

    private void UpdateVolume()
    {
        masterVolume = masterVolSlider.value;
        AudioListener.volume = masterVolume;
        handler.masterVolumeValue = AudioListener.volume;
    }

    private void UpdateTimerToggle()
    {
        handler.timerBool = speedRunTimerToggle.isOn;
    }
}
