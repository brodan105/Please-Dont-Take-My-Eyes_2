using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    TallyCountManager _tallyManager;

    public TMP_Text timeCounter;

    public string timePlayingStr;

    public TimeSpan timePlaying;
    public bool timerGoing;

    private float elapsedTime;

    private void Awake()
    {
        instance = this;

        _tallyManager = GameObject.FindAnyObjectByType<TallyCountManager>();
    }

    private void Update()
    {
        if(_tallyManager == null)
        {
            _tallyManager = GameObject.FindAnyObjectByType<TallyCountManager>();
        }
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

        // Use this later to add up splits when game / levels for boss are done
        _tallyManager.levelSplits.Add(timePlayingStr);
    }

    public void ReloadTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timePlayingStr = timePlaying.ToString("mm':'ss':'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}
