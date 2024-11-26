using System.Collections.Generic;
using UnityEngine;

public class ToggleInvisible : MonoBehaviour
{
    public List<GameObject> objs;

    public void TurnInvisible()
    {
        foreach(GameObject obj in objs)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void TurnVisible()
    {
        foreach (GameObject obj in objs)
        {
            obj.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
