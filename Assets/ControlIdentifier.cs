using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlIdentifier : MonoBehaviour
{
    public static ControlIdentifier instance;

    [SerializeField] List<GameObject> gamepadIcons;
    [SerializeField] List<GameObject> keyIcons;

    bool hasController;

    private void Awake()
    {
        instance = this;

        UpdateInputDevice();
    }

    private void Update()
    {
        // Automatically update input device
        if ((hasController && Gamepad.current == null) || (!hasController && Gamepad.current != null))
        {
            UpdateInputDevice();
        }
    }

    public void UpdateInputDevice()
    {
        if (Gamepad.current != null)
        {
            hasController = true;

            // Set cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if(gamepadIcons.Count > 0)
            {
                foreach (GameObject pad in gamepadIcons)
                {
                    pad.SetActive(true);
                }
            }

            if(keyIcons.Count > 0)
            {
                foreach (GameObject key in keyIcons)
                {
                    key.SetActive(false);
                }
            }
        }
        else
        {
            hasController = false;

            // Set cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (gamepadIcons.Count > 0)
            {
                foreach (GameObject pad in gamepadIcons)
                {
                    pad.SetActive(false);
                }
            }

            if (keyIcons.Count > 0)
            {
                foreach (GameObject key in keyIcons)
                {
                    key.SetActive(true);
                }
            }
        }
    }
}
