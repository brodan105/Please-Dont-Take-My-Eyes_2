using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuManager : MonoBehaviour
{
    bool isPaused = false;

    [Header("References")]
    [SerializeField] EventSystem _event;
    [SerializeField] GameObject firstSelectedButton_Pause, firstSelectedButton_Option;
    [SerializeField] GameObject _pauseMenu, pulseCooldown;
    [SerializeField] GameObject _pausePanel, _optionPanel;

    [SerializeField] AudioListener playerAudioListener;

    bool playerCouldMoveBeforePause;

    private void Start()
    {
        CheckPause();
    }

    public void PauseButton(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        CheckPause();
    }

    void CheckPause()
    {
        if (isPaused)
        {  
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            pulseCooldown.SetActive(false);

            playerAudioListener.enabled = false;

            if (PlayerMovement.instance.canMove)
            {
                playerCouldMoveBeforePause = true;
            }
            else
            {
                playerCouldMoveBeforePause = false;
            }

            PlayerMovement.instance.StopMovement();

            if(Gamepad.current == null)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            OpenPausePanel();
        }
        else
        {
            OpenPausePanel();

            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
            pulseCooldown.SetActive(true);

            playerAudioListener.enabled = true;

            if (playerCouldMoveBeforePause)
            {
                PlayerMovement.instance.StartMovement();
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OpenPausePanel()
    {
        _optionPanel.SetActive(false);
        _pausePanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);

        _event.SetSelectedGameObject(firstSelectedButton_Pause);
    }

    public void OpenOptionsPanel()
    {
        _optionPanel.SetActive(true);
        _pausePanel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);

        _event.SetSelectedGameObject(firstSelectedButton_Option);
    }
}
