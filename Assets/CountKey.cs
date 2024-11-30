using UnityEngine;

public class CountKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(KeyCounter.instance != null)
            {
                KeyCounter.instance.AddKey();
            }
        }
    }
}
