using UnityEngine;
using TMPro;
using System.Collections;

public class CreditController : MonoBehaviour
{
    [SerializeField] TMP_Text collectableCount;
    [SerializeField] TMP_Text enemyCount;
    [SerializeField] TMP_Text deathCount;

    TallyCountManager _tallyManager;

    [SerializeField] GameObject splitTextPrefab;
    [SerializeField] Transform splitPanelGroup;

    private void Update()
    {
        if(_tallyManager == null)
        {
            _tallyManager = GameObject.FindAnyObjectByType<TallyCountManager>();
        }
    }

    public void UpdateStats()
    {
        collectableCount.text = "You found " + _tallyManager.collectableCount + " collectables.";
        enemyCount.text = "You defeated " + _tallyManager.enemyCount + " enemies.";
        deathCount.text = "You died " + _tallyManager.deathCount + " times.";

        foreach(string split in _tallyManager.levelSplits)
        {
            var s = Instantiate(splitTextPrefab, splitPanelGroup);
            s.GetComponent<TMP_Text>().text = split;
        }
    }

    public void GoToMainMenu()
    {
        SceneController.instance.ReturnToMainMenu();
    }
}
