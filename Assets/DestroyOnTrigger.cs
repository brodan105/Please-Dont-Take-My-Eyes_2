using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DestroyOnTrigger : MonoBehaviour
{
    [Header("Optional Event")]
    [SerializeField] UnityEvent _event;

    [SerializeField] bool destroyParent = false;
    [SerializeField] GameObject parentObject;

    [SerializeField] float delay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (_event == null) return;
            else
                _event.Invoke();

            StartCoroutine(destroyDelay());
        }
    }

    IEnumerator destroyDelay()
    {
        yield return new WaitForSeconds(delay);

        if (destroyParent && parentObject != null)
        {
            Destroy(parentObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
