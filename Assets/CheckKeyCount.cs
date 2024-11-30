using UnityEngine;
using UnityEngine.Events;

public class CheckKeyCount : MonoBehaviour
{
    public enum triggerMethod { function, onTriggerEnter, onTriggerExit }
    public triggerMethod method = triggerMethod.function;

    [SerializeField] int keysRequired;

    [SerializeField] UnityEvent _enoughEvent;
    [SerializeField] UnityEvent _notEnoughEvent;

    public void CheckKeys()
    {
        if (KeyCounter.instance == null && method == triggerMethod.function) return;

        if(KeyCounter.instance.keyCount >= keysRequired)
        {
            if(_enoughEvent != null)
            {
                _enoughEvent.Invoke();
            }
        }
        else
        {
            if(_notEnoughEvent != null)
            {
                _notEnoughEvent.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && KeyCounter.instance != null && method == triggerMethod.onTriggerEnter)
        {
            if (KeyCounter.instance.keyCount >= keysRequired)
            {
                if (_enoughEvent != null)
                {
                    _enoughEvent.Invoke();
                }
            }
            else
            {
                if (_notEnoughEvent != null)
                {
                    _notEnoughEvent.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && KeyCounter.instance != null && method == triggerMethod.onTriggerExit)
        {
            if (KeyCounter.instance.keyCount >= keysRequired)
            {
                if (_enoughEvent != null)
                {
                    _enoughEvent.Invoke();
                }
            }
            else
            {
                if (_notEnoughEvent != null)
                {
                    _notEnoughEvent.Invoke();
                }
            }
        }
    }
}
