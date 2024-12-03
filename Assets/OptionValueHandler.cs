using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionValueHandler : MonoBehaviour
{
    // Sets Default Values and Holds them to be changed by option controller scripts
    public float masterVolumeValue = 1;
    public float sfxVolumeValue = 1;
    public float uiVolumeValue = 1;
    public float backgroundVolumeValue = 1;
    //public float sfxSliderValue = 1;

    public bool timerBool;

    private void Start()
    {
        DontDestroyOnLoad(this);

        AudioListener.volume = masterVolumeValue;
    }
}
