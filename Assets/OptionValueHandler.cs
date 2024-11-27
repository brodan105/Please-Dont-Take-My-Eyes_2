using System.Collections.Generic;
using UnityEngine;

public class OptionValueHandler : MonoBehaviour
{
    // Sets Default Values and Holds them to be changed by option controller scripts
    public float masterVolumeValue = 1;

    public bool timerBool;

    private void Start()
    {
        DontDestroyOnLoad(this);

        AudioListener.volume = masterVolumeValue;
    }
}
