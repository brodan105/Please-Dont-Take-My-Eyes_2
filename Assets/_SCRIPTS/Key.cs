using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] UnityEvent collectKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collectKey.Invoke();
            Destroy(gameObject);
        }
    }
}
