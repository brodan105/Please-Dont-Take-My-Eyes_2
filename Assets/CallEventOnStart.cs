using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CallEventOnStart : MonoBehaviour
{
    [SerializeField] UnityEvent startEvent;

    public enum whenToCall { onStart, onAwake, function }
    public whenToCall callState = whenToCall.onStart;

    [SerializeField] float delay;

    private void Start()
    {
        if (callState == whenToCall.onStart)
            StartCoroutine(eventDelay());
    }

    private void Awake()
    {
        if (callState == whenToCall.onAwake)
            StartCoroutine(eventDelay());
    }

    public void ActivateEvent()
    {
        if(callState == whenToCall.function)
            StartCoroutine(eventDelay());
    }

    private IEnumerator eventDelay()
    {
        yield return new WaitForSeconds(delay);
        startEvent.Invoke();
    }
}
