using UnityEngine;
using UnityEngine.Events;

public class ReferenceFunction : MonoBehaviour
{
    [SerializeField] UnityEvent _event;

    public void PlayEvent()
    {
        _event.Invoke();
    }
}
