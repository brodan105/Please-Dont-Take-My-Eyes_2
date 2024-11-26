using System.Collections.Generic;
using UnityEngine;

public class EnableAttackBoxes : MonoBehaviour
{
    [SerializeField] List<GameObject> enableObjs;
    [SerializeField] List<GameObject> disableObjs;

    public void EnableThis()
    {
        foreach(GameObject obj in enableObjs)
        {
            obj.SetActive(true);
        }
    }

    public void DisableThis()
    {
        foreach(GameObject obj in disableObjs)
        {
            obj.SetActive(false);
        }
    }
}
