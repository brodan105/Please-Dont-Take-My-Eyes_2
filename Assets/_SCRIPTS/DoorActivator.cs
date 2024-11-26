using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorActivator : MonoBehaviour
{
    Animator anim;

    bool unlocked = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        if (unlocked)
        {
            anim.SetTrigger("Open");
        }
    }

    public void CloseDoor()
    {
        anim.SetTrigger("Close");
    }

    public void UnlockDoor()
    {
        unlocked = true;
    }
}
