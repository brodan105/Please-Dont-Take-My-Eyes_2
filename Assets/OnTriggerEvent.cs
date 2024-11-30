using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    public enum triggerType { OnTriggerEnter, OnTriggerExit };
    public triggerType state;

    [Header("Events")]
    [SerializeField] UnityEvent _event;

    [Header("Adjustments")]
    [SerializeField] bool oneTimeUse = true;
    [SerializeField] float activationDelay;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state != triggerType.OnTriggerEnter) return;
        if(collision.tag == "Player")
        {
            StartCoroutine(delayTimer());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (state != triggerType.OnTriggerExit) return;
        if (collision.tag == "Player")
        {
            StartCoroutine(delayTimer());
        }
    }

    IEnumerator delayTimer()
    {
        // delay timer
        yield return new WaitForSeconds(activationDelay);

        // invoke any event set
        _event.Invoke();

        if (oneTimeUse)
        {
            // Destroy this instance of script
            Destroy(this);
        }
    }
}
