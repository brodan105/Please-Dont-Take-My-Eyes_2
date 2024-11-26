using UnityEngine;
using UnityEngine.Events;

public class CallEventOnStart : MonoBehaviour
{
    [SerializeField] UnityEvent startEvent;

    private void Start()
    {
        startEvent.Invoke();
    }
}
