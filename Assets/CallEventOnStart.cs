using UnityEngine;
using UnityEngine.Events;

public class CallEventOnStart : MonoBehaviour
{
    [SerializeField] UnityEvent startEvent;

    public enum whenToCall { onStart, onAwake, function }
    public whenToCall callState = whenToCall.onStart;

    private void Start()
    {
        if(callState == whenToCall.onStart)
            startEvent.Invoke();
    }

    private void Awake()
    {
        if (callState == whenToCall.onAwake)
            startEvent.Invoke();
    }

    public void ActivateEvent()
    {
        if(callState == whenToCall.function)
        {
            startEvent.Invoke();
        }
    }
}
