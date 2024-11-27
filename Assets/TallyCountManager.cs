using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TallyCountManager : MonoBehaviour
{
    public static TallyCountManager instance;

    public TimeController _timeControl;
    public PauseMenuReference _pauseReference;

    public int collectableCount;
    public int enemyCount;
    public int deathCount;

    public List<string> levelSplits;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void UpdateTally()
    {
        _pauseReference.collectableText.text = "Collectables Gathered: " + collectableCount;
        _pauseReference.enemyText.text = "Enemies Defeated: " + enemyCount;
        _pauseReference.deathText.text = "Player Deaths: " + deathCount;

        if(!_timeControl.timerGoing)
        {
            _pauseReference.timeText.text = "Current Level Split: 00:00:00";
        }
        else
        {
            _pauseReference.timeText.text = "Current Level Split: " + _timeControl.timePlayingStr;
        }
    }
}
