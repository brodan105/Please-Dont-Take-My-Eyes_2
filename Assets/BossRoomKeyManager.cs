using UnityEngine;
using UnityEngine.Events;

public class BossRoomKeyManager : MonoBehaviour
{
    [SerializeField] UnityEvent _event;
    [SerializeField] int keysNeeded = 1;
    int keysObtained;

    public void AddKey()
    {
        Debug.Log("GOT A KEY");
        keysObtained++;

        if(keysObtained == keysNeeded)
        {
            _event.Invoke();
            Debug.Log("Door Unlocked");
        }
    }

    public void LockedDoorInteract()
    {
        Debug.Log("Door is locked");
    }
}
