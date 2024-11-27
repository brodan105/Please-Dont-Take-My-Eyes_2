using TMPro;
using UnityEngine;

public class PauseMenuReference : MonoBehaviour
{
    public static PauseMenuReference instance;

    public TMP_Text collectableText;
    public TMP_Text enemyText;
    public TMP_Text deathText;
    public TMP_Text timeText;

    public GameObject speedRunTimer;

    private void Awake()
    {
        instance = this;
    }
}
