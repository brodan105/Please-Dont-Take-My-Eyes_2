using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    public TMP_Text timeCounter;

    public TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeCounter.text = "00:00:00";
        timerGoing = false;
    }

    public void StartTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void ResetTimer()
    {
        timeCounter.text = "00:00:00";
    }

    public void StopTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss':'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}
