using UnityEngine;
using UnityEngine.Events;

public class BossRoomKeyManager : MonoBehaviour
{
    [SerializeField] UnityEvent _event;
    [SerializeField] int keysNeeded = 1;
    int keysObtained;

    bool allKeysObtained;

    private void Update()
    {
        if(KeyCounter.instance != null)
        {
            if (!allKeysObtained)
            {
                keysObtained = KeyCounter.instance.keyCount;

                if(keysObtained == keysNeeded)
                {
                    allKeysObtained = true;
                    _event.Invoke();
                }
            }
        }
    }

    public void LockedDoorInteract()
    {
        Debug.Log("Door is locked");
    }
}
