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

    AudioSource pauseAudio;
    AudioSource[] _audioSources;

    bool playerCouldMoveBeforePause;

    private void Start()
    {
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
        //AudioListener.pause = false;
        UnmuteAudio();

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
            //AudioListener.pause = true;
            MuteAudio();

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
}
