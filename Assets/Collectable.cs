using UnityEngine;

public class Collectable : MonoBehaviour
{

    public void DestroyThis()
    {
        // Update tally count
        TallyCountManager.instance.collectableCount++;

        Destroy(gameObject);
    }
}
