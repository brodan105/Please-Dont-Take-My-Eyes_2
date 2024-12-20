using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{

    bool inTrigger;

    [SerializeField] bool hiddenSwitch = false;

    [SerializeField] bool OneTime;
    bool destroyActivated = false;
    [SerializeField] bool OneTimeDestroy;
    [SerializeField] bool locked;
    bool canActivate = true;
    bool hasActivated;

    [SerializeField] public UnityEvent _unlockedEvent;
    [SerializeField] public UnityEvent _lockedEvent;
    [SerializeField] PlayerInput _input;
    InputAction interactAction;

    [SerializeField] GameObject keyGUI, buttonGUI;

    private void Awake()
    {
        interactAction = _input.actions["Interact"];
    }

    private void Update()
    {
        if(inTrigger && interactAction.WasPressedThisFrame() && !hasActivated)
        {
            if (!canActivate) return;

            if (locked)
            {
                _lockedEvent.Invoke();
            }
            else
            {
                _unlockedEvent.Invoke();

                if (OneTime)
                {
                    hasActivated = true;
                }

                if (OneTimeDestroy)
                {
                    DestroyComponent();
                }
            }
            StartCoroutine(interactCooldown());
        }
    }

    public void ResetOneTime()
    {
        hasActivated = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (destroyActivated && OneTimeDestroy) return;

        if(collision.tag == "Player" && !hasActivated)
        {
            inTrigger = true;

            EnableGUI();
        }

        if (hasActivated)
        {
            DisableGUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = false;
        }

        DisableGUI();
    }

    public void EnableHidden()
    {
        hiddenSwitch = true;
    }

    public void DisableHidden()
    {
        hiddenSwitch = false;
    }

    private void EnableGUI()
    {
        if (hiddenSwitch) return;

        if (Gamepad.current == null)
        {
            keyGUI.SetActive(true);
        }
        else
        {
            buttonGUI.SetActive(true);
        }
    }

    void DisableGUI()
    {
        if (hiddenSwitch) return;

        if (Gamepad.current == null)
        {
            keyGUI.SetActive(false);
        }
        else
        {
            buttonGUI.SetActive(false);
        }
    }

    public void UnlockDoor()
    {
        locked = false;
    }

    public void Lock()
    {
        locked = true;
    }

    public void DestroyComponent()
    {
        DisableGUI();
        Destroy(this);
    }

    IEnumerator interactCooldown()
    {
        canActivate = false;
        yield return new WaitForSeconds(1);
        canActivate = true;
    }
}
