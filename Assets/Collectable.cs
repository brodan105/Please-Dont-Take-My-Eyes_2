using UnityEngine;

public class Collectable : MonoBehaviour
{

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
