using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnFromID : MonoBehaviour
{
    // Build settings scene id number from where you're coming from
    public int spawnFromBuildID;

    private void Awake()
    {
        if (SceneTrackerManager.instance.previousScene == SceneManager.GetActiveScene().buildIndex) return;

        if(SceneTrackerManager.instance.previousScene == spawnFromBuildID)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = this.transform.position;

            Debug.Log("Player has spawned at position: " + spawnFromBuildID);
        }
    }
}
