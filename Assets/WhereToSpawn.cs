using System.Collections.Generic;
using UnityEngine;

public class WhereToSpawn : MonoBehaviour
{
    public static WhereToSpawn instance;

    public int sceneComingFromNum;

    [SerializeField] List<Transform> spawnPoints;

    GameObject player;

    private void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");

        if (spawnPoints.Count == 0) return;

        foreach(Transform spawn in spawnPoints)
        {
            if(spawn.GetComponent<PlayerSpawnFromID>().spawnFromBuildID == sceneComingFromNum)
            {
                player.transform.position = spawn.position;
            }
        }
    }
}
