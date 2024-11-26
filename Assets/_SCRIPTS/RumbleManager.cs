using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;

    private Gamepad pad;

    private Coroutine stopRumbleAfterTimeCoroutine;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void StartRumble(float lowFrequency, float highFrequency)
    {
        pad.SetMotorSpeeds(lowFrequency, highFrequency);
    }

    public void StopRumble()
    {
        pad.SetMotorSpeeds(0, 0);
    }

    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        // get reference to gamepad
        pad = Gamepad.current;

        // if we have a current gamepad

        if(pad != null)
        {
            // start rumble
            pad.SetMotorSpeeds(lowFrequency, highFrequency);

            // stop rumble after duration
            stopRumbleAfterTimeCoroutine = StartCoroutine(StopRumble(duration, pad));
        }
    }

    private IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // once duration is over

        pad.SetMotorSpeeds(0, 0);
    }
}
