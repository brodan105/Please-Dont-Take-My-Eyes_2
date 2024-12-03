using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager instance;

    public bool isPaused = false;
    bool canPause = true;

    TallyCountManager tallyManager;
    [SerializeField] GameObject tallyManagerPrefab;

    [Header("References")]
    [SerializeField] EventSystem _event;
    [SerializeField] GameObject firstSelectedButton_Pause, firstSelectedButton_Option;
    [SerializeField] GameObject _pauseMenu, pulseCooldown;
    [SerializeField] GameObject _pausePanel, _optionPanel;

    AudioSource pauseAudio;
    AudioSource[] _audioSources;

    bool playerCouldMoveBeforePause;

    private void Start()
    {
        instance = this;
        CheckPause();
    }

    private void Awake()
    {
        tallyManager = FindAnyObjectByType<TallyCountManager>();

        if(tallyManager == null)
        {
            tallyManager = Instantiate(tallyManagerPrefab.GetComponent<TallyCountManager>());
        }

        tallyManager._timeControl = FindAnyObjectByType<TimeController>();
        tallyManager._pauseReference = FindAnyObjectByType<PauseMenuReference>();

        // Audio
        _audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        pauseAudio = GetComponent<AudioSource>();
        UnmuteAudio();
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
        if (canPause)
        {
            isPaused = !isPaused;

            CheckPause();
        }
    }

    void CheckPause()
    {
        Pause();
    }

    public void Unpause()
    {
        OpenPausePanel();

        //AudioListener.pause = false;
        UnmuteAudio();

        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        pulseCooldown.SetActive(true);

        // Update tally counts
        tallyManager.UpdateTally();

        if (playerCouldMoveBeforePause)
        {
            PlayerMovement.instance.StartMovement();
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void disablePause()
    {
        canPause = false;
    }

    public void Pause()
    {
        if (isPaused)
        {
            //AudioListener.pause = true;
            MuteAudio();

            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            pulseCooldown.SetActive(false);

            if(PlayerMovement.instance != null)
            {
                if (PlayerMovement.instance.canMove)
                {
                    playerCouldMoveBeforePause = true;
                }
                else
                {
                    playerCouldMoveBeforePause = false;
                }

                PlayerMovement.instance.StopMovement();
            }

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

    void MuteAudio()
    {
        foreach(AudioSource s in _audioSources)
        {
            if(s != pauseAudio && s.enabled)
            {
                s.mute = true;
            }
            else if(s == pauseAudio)
            {
                s.mute = false;
            }
        }
    }

    void UnmuteAudio()
    {
        foreach (AudioSource s in _audioSources)
        {
            if (s != pauseAudio && s.enabled)
            {
                s.mute = false;
            }
            else if (s == pauseAudio)
            {
                s.mute = true;
            }
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

    public void ReturningToMainMenu()
    {
        if(PlayerMovement.instance != null)
        {
            PlayerMovement.instance.StopMovement();
        }
    }
}
