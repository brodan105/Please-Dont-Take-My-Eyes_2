using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    bool isPaused = false;

    TallyCountManager tallyManager;
    [SerializeField] GameObject tallyManagerPrefab;

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

    private void Awake()
    {
        tallyManager = GameObject.FindAnyObjectByType<TallyCountManager>();

        if(tallyManager == null)
        {
            tallyManager = Instantiate(tallyManagerPrefab.GetComponent<TallyCountManager>());
        }

        tallyManager._timeControl = GameObject.FindAnyObjectByType<TimeController>();
        tallyManager._pauseReference = GameObject.FindAnyObjectByType<PauseMenuReference>();
    }

    public void UnStuckPlayer()
    {
        PlayerDie.instance.Die();

        if (isPaused)
        {
            isPaused = !isPaused;
            CheckPause();
        }
    }

    public void PauseButton(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        CheckPause();
    }

    void CheckPause()
    {
        Pause();
    }

    public void Unpause()
    {
        OpenPausePanel();

        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        pulseCooldown.SetActive(true);

        // Update tally counts
        tallyManager.UpdateTally();

        //playerAudioListener.enabled = true;
        AudioListener.pause = false;

        if (playerCouldMoveBeforePause)
        {
            PlayerMovement.instance.StartMovement();
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            pulseCooldown.SetActive(false);

            //playerAudioListener.enabled = false;
            AudioListener.pause = true;

            if (PlayerMovement.instance.canMove)
            {
                playerCouldMoveBeforePause = true;
            }
            else
            {
                playerCouldMoveBeforePause = false;
            }

            PlayerMovement.instance.StopMovement();

            if (Gamepad.current == null)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            OpenPausePanel();
        }
        else
        {
            Unpause();
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
