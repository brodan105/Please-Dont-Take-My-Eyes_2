using System.Collections.Generic;
using UnityEngine;

public class EnableDisableObjs : MonoBehaviour
{
    [SerializeField] List<GameObject> disableObj;
    [SerializeField] List<GameObject> enableObj;

    public void DisableObjects()
    {
        foreach(GameObject obj in disableObj)
        {
            obj.SetActive(false);
        }
    }

    public void EnableObjects()
    {
        foreach(GameObject obj in enableObj)
        {
            obj.SetActive(true);
        }
    }
}
